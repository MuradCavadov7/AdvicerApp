using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.DTOs.StatusCommentDto;
using AdvicerApp.BL.DTOs.StatusDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace AdvicerApp.BL.Services.Implements;

public class StatusService(IStatusRepository _repo, ICurrentUser _user, IWebHostEnvironment _env) : IStatusService
{
    private string _userId = _user.GetId();
    public async Task<int> CreateAsync(StatusCreateDto dto)
    {
        if (dto.StatusImage != null)
        {
            if (!dto.StatusImage.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.StatusImage.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }

        var recentStatuses = await _repo.GetAllAsync(x => new { x.Id, x.CreatedTime }, false, false);
        var hasRecentStatus = recentStatuses.Any(x => x.CreatedTime >= DateTime.UtcNow.AddHours(-24));

        if (hasRecentStatus)
        {
            throw new InvalidOperationException("You can only share one status per day.");
        }
        var status = new Status
        {
            Content = dto.Content,
            UserId = _userId,
            CreatedTime = DateTime.UtcNow,
            ExpiredDate = DateTime.UtcNow.AddHours(24)
        };
        if (dto.StatusImage != null)
        {
            status.Image = await dto.StatusImage.UploadAsync(_env.WebRootPath, "imgs", "status");
        }
        else
        {
            status.Image = null;
        }
        await _repo.AddAsync(status);
        await _repo.SaveAsync();
        return status.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var status = await _repo.GetByIdAsync(id, x => x, false, false);
        if (status is null) throw new NotFoundException<Status>();
        if (!string.IsNullOrEmpty(status.Image))
        {
            string imagePath = Path.Combine(_env.WebRootPath, "imgs", "status", status.Image);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        await _repo.DeleteAndSaveAsync(id);
    }

    public async Task<IEnumerable<StatusGetDto>> GetAllAsync()
    {
        var statuses = await _repo.GetAllAsync(x => new StatusGetDto
        {
            Id = x.Id,
            Content = x.Content,
            UserId = x.UserId,
            StatusImage = x.Image,
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
            StatusImage = x.Image,
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
        var status = await _repo.GetByIdAsync(id, x => x, false, false);
        if (status is null) throw new NotFoundException<Status>();
        status.Content = dto.Content;
        if (!string.IsNullOrEmpty(status.Image))
        {
            string imagePath = Path.Combine(_env.WebRootPath, "imgs", "status", status.Image);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        if (dto.StatusImage != null)
        {
            status.Image = await dto.StatusImage.UploadAsync(_env.WebRootPath, "imgs", "status");
        }
        else
        {
            status.Image = null;
        }
        await _repo.SaveAsync();
    }
}
