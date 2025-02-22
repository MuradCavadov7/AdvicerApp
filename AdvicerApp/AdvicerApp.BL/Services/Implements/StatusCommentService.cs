using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.DTOs.StatusCommentDto;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;

namespace AdvicerApp.BL.Services.Implements;

public class StatusCommentService(IStatusCommentRepository _repo, ICurrentUser _user,IStatusRepository _stRepo) : IStatusCommentService
{
    private string _userId = _user.GetId();
    public async Task<int> CreateAsync(StatusCommentCreateDto dto)
    {
        if (!await _stRepo.IsExistAsync(dto.StatusId)) throw new NotFoundException<Status>();
        var statusComment = new StatusComment
        {
            Content = dto.Content,
            StatusId = dto.ParentId.HasValue ? (await _repo.GetByIdAsync(dto.ParentId.Value, x => x, false, false))!.StatusId : dto.StatusId,
            UserId = _userId,
            ParentId = dto.ParentId 
        };
        await _repo.AddAsync(statusComment);
        await _repo.SaveAsync();
        return statusComment.Id;
    }
    

    public async Task DeleteAsync(int id)
    {
        var stComment = await _repo.GetByIdAsync(id, q => q, true, false);
        if (stComment == null) throw new NotFoundException<StatusComment>();

        if (stComment.Children != null)
        {
            foreach (var reply in stComment.Children)
            {
                _repo.Delete(reply);
            }
        }


        _repo.Delete(stComment);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<StatusCommentGetDto>> GetAllAsync()
    {
        var stComments = await _repo.GetAllAsync(x => new StatusCommentGetDto
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Content = x.Content,
            Username = x.User.UserName,

            Replies = x.Children.Select(r => new StatusCommentGetDto
            {
                Id = r.Id,
                Content = r.Content,
                Username = r.User.UserName,
                ParentId = r.ParentId,

            }).ToList()
        }, true, false);
        return stComments;
    }

    public async Task<StatusCommentGetDto> GetByIdAsync(int id)
    {
        var stComment = await _repo.GetByIdAsync(id, x => new StatusCommentGetDto
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Content = x.Content,
            Username = x.User.UserName,

            Replies = x.Children.Select(r => new StatusCommentGetDto
            {
                Id = r.Id,
                Content = r.Content,
                Username = r.User.UserName,
                ParentId = r.ParentId,

            }).ToList()
        }, true, false);
        if (stComment is null) throw new NotFoundException<StatusComment>();
        return stComment;
    }

    public async Task UpdateAsync(int id, StatusCommentUpdateDto dto)
    {
        var stComment = await _repo.GetByIdAsync(id, x => x, false,false);
        if (stComment is null) throw new NotFoundException<StatusComment>();
        stComment.Content = dto.Content;
        stComment.UpdatedTime = DateTime.UtcNow;
        await _repo.SaveAsync();
    }
}
