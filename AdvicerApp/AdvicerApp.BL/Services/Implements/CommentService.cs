using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Enums;
using AdvicerApp.Core.Repositories;
using AutoMapper;

namespace AdvicerApp.BL.Services.Implements;

public class CommentService(ICommentRepository _repo, ICurrentUser _user, IRestaurantRepository _restRepo, IMapper _mapper) : ICommentService
{
    private string _userId = _user.GetId();
    private string _userRole = _user.GetRole();
    public async Task<int> CreateAsync(CommentCreateDto dto)
    {
        if (!await _restRepo.IsExistAsync(dto.RestaurantId)) throw new NotFoundException<Restaurant>();
        var comment = _mapper.Map<Comment>(dto);
        comment.UserId = _userId;
        if (_userRole == nameof(Role.Owner))
        {
            if (!dto.ParentId.HasValue)
                throw new UnAuthorizedAccessException("Owners can only reply to comments, not create new ones.");

            var parentComment = await _repo.GetByIdAsync(dto.ParentId.Value, x => x, false, false);
            if (parentComment == null)
                throw new NotFoundException<Comment>("Parent comment not found.");

            var restaurant = await _restRepo.GetByIdAsync(parentComment.RestaurantId, x => x, false, false);
            if (restaurant == null || restaurant.OwnerId != _userId)
                throw new UnAuthorizedAccessException("You can only reply to comments on your own restaurant.");
        }
        else
        {

            if (dto.ParentId.HasValue)
                throw new UnAuthorizedAccessException("Users cannot reply to comments.");
        }

        comment.RestaurantId = dto.ParentId.HasValue ? (await _repo.GetByIdAsync(dto.ParentId.Value, x => x, false, false))!.RestaurantId : dto.RestaurantId;
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
        if (comment is null) throw new NotFoundException<Comment>();
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
        comment.UserId = _userId;
        if (_userRole == nameof(Role.Owner))
        {
            var restaurant = await _restRepo.GetByIdAsync(comment.RestaurantId, x => x, false, false);
            if (restaurant == null || restaurant.OwnerId != _userId)
                throw new UnAuthorizedAccessException("You can only edit replies on your own restaurant.");
        }
        else
        {
            if (comment.UserId != _userId)
                throw new UnAuthorizedAccessException("You can only edit your own comments.");
        }
        _mapper.Map(dto, comment);
        comment.UpdatedTime = DateTime.UtcNow;
        await _repo.AddAsync(comment);
        await _repo.SaveAsync();
    }
}
