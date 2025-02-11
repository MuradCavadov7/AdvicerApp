using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AutoMapper;
using System.Xml.Linq;

namespace AdvicerApp.BL.Services.Implements;

public class CommentService(ICommentRepository _repo, ICurrentUser _user, IRestaurantRepository _restRepo, IMapper _mapper) : ICommentService
{
    private string _userId = _user.GetId();
    public async Task<int> CreateAsync(CommentCreateDto dto)
    {
        if (!await _restRepo.IsExistAsync(dto.RestaurantId)) throw new NotFoundException<Restaurant>();
        var comment = _mapper.Map<Comment>(dto);
        if (comment.UserId != _user.GetId()) throw new UnAuthorizedAccessException("You can only edit your own comments.");
        Comment? parent = null;
        if (dto.ParentId.HasValue)
        {
            parent = await _repo.GetByIdAsync(dto.ParentId.Value, x => x.Parent, false, false);
            if (parent is null)
                throw new NotFoundException<Comment>();
        }
        comment.RestaurantId = parent?.RestaurantId ?? dto.RestaurantId;
        await _repo.AddAsync(comment);
        await _repo.SaveAsync();
        return comment.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _repo.GetByIdAsync(id, x => new Comment { Id = x.Id }, false, false);
        if (comment == null) throw new NotFoundException<Comment>();
        _repo.Delete(comment);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<CommentGetDto>> GetAllAsync()
    {
        var comments = await _repo.GetAllAsync(x => new CommentGetDto
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Text = x.Text,
            Username = x.User.UserName,
            Replies = x.Children.Select(r => new CommentGetDto
            {
                Id = r.Id,
                Text = r.Text,
                Username = r.User.UserName,
                ParentId = r.ParentId
            }).ToList()
        }, true, false);
        return comments;
    }

    public async Task<CommentGetDto> GetByIdAsync(int id)
    {
        var comment = await _repo.GetByIdAsync(id, x => new CommentGetDto
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Text = x.Text,
            Username = x.User.UserName,
            Replies = x.Children.Select(r => new CommentGetDto
            {
                Id = r.Id,
                Text = r.Text,
                Username = r.User.UserName,
                ParentId = r.ParentId
            }).ToList()
        }, true, false);
        return comment;
    }

    public async Task UpdateAsync(int id, CommentUpdateDto dto)
    {
        var comment = await _repo.GetByIdAsync(id, x => new Comment
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Children = x.Children
        }, false, false);
        if (comment == null) throw new NotFoundException<Comment>();
        if (comment.UserId != _user.GetId()) throw new UnAuthorizedAccessException("You can only edit your own comments.");
        _mapper.Map(dto, comment);
        comment.UpdatedTime = DateTime.UtcNow;
        await _repo.AddAsync(comment);
        await _repo.SaveAsync();
    }
}
