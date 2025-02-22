using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.DTOs.StatusCommentDto;
using AdvicerApp.BL.DTOs.StatusDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;

namespace AdvicerApp.BL.Services.Implements;

public class StatusService(IStatusRepository _repo,ICurrentUser _user) : IStatusService
{
    private string _userId = _user.GetId();
    public async Task<int> CreateAsync(StatusCreateDto dto)
    {
        var status = new Status
        {
            Content = dto.Content,
            UserId = _userId
        };
        await _repo.AddAsync(status);
        await _repo.SaveAsync();
        return status.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var status = await _repo.GetByIdAsync(id, x => new Status { Id = x.Id }, false, false);
        if (status is null) throw new NotFoundException<Status>();
        await _repo.DeleteAndSaveAsync(id);
    }

    public async Task<IEnumerable<StatusGetDto>> GetAllAsync()
    {
        var statuses = await _repo.GetAllAsync(x => new StatusGetDto
        {
            Id = x.Id,
            Content = x.Content,
            UserId = x.UserId,
            StatusComments = x.StatusComments.Where(c => c.ParentId == 0).Take(5)
            .Select(c => new StatusCommentGetDto
            {
                Id = c.Id,
                Content = c.Content
            }).ToList()
        }, true, false);
        return statuses;
    }

    public async Task<StatusGetDto> GetByIdAsync(int id)
    {
        var status = await _repo.GetByIdAsync(id, x => new StatusGetDto
        {
            Id = x.Id,
            Content = x.Content,
            UserId = x.UserId,
            StatusComments = x.StatusComments.Where(c => c.ParentId == 0).Take(5)
            .Select(c => new StatusCommentGetDto
            {
                Id = c.Id,
                Content = c.Content
            }).ToList()
        }, true, false);
        if (status is null) throw new NotFoundException<Status>();
        return status;

    }

    public async Task UpdateAsync(int id, StatusUpdateDto dto)
    {
        var status = await _repo.GetByIdAsync(id, x =>x ,false, false);
        if (status is null) throw new NotFoundException<Status>();
        status.Content = dto.Content;
        await _repo.SaveAsync();
    }
}
