﻿using AdvicerApp.BL.DTOs.ReportDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Enums;
using AdvicerApp.Core.Repositories;

namespace AdvicerApp.BL.Services.Implements;

public class ReportService(IReportRepository _repo,ICurrentUser _user, ICommentRepository _comRepo,IRestaurantRepository _restRepo) : IReportService
{
    private string _userId = _user.GetId();
    private string _userRole = _user.GetRole();
    public async Task<int> CreateAsync(ReportCreateDto dto)
    {
        if (_userRole != nameof(Role.Owner)) 
            throw new UnauthorizedAccessException("Only Owner report comment");

        var comment = await _comRepo.GetByIdAsync(dto.CommentId,x =>x, false,false);
        if (comment == null)
            throw new NotFoundException<Comment>("Comment not found.");

        var restaurant = await _restRepo.GetByIdAsync(comment.RestaurantId,x=>x,false,false);
        if (restaurant == null)
            throw new NotFoundException<Restaurant>("Restaurant not found.");

        if (restaurant.OwnerId != _userId)
            throw new UnauthorizedAccessException("You can only report comments from your own restaurant.");

        if (!BadWordsFilter.ContainsBadWords(comment.Text))
            throw new BadRequestException("This comment cannot be reported because it is not an offensive or defamatory comment.");

        var report = new Report
        {
            OwnerId = _userId,
            CommmentId = dto.CommentId,
            Reason = dto.Reason,
            IsResolved = false,
        };

        await _repo.AddAsync(report);
        await _repo.SaveAsync();

        return report.Id;
    }

    public async Task<IEnumerable<ReportGetDto>> GetAllAsync()
    {
        var reports = await _repo.GetAllAsync(x => new ReportGetDto
        {
            Id = x.Id,
            OwnerId = x.OwnerId,
            CommentId = x.CommmentId ?? 0,
            CommentText = x.Commment != null ? x.Commment.Text : "Deleted Comment",
            Reason = x.Reason,
            IsResolved = x.IsResolved
        },true,false);

        return reports;
    }

    public async Task<ReportGetDto> GetByIdAsync(int id)
    {
        var report = await _repo.GetByIdAsync(id, x => new ReportGetDto
        {
            Id = x.Id,
            OwnerId = x.OwnerId,
            CommentId = x.CommmentId ?? 0,
            CommentText = x.Commment != null ? x.Commment.Text : "Deleted Comment",
            Reason = x.Reason,
            IsResolved = x.IsResolved
        }, true, false);
        if (report is null) throw new NotFoundException<Report>();
        return report;
    }

    public async Task ResolveAsync(int id)
    {
        var report = await _repo.GetByIdAsync(id, x => x, true, false);
        if (report == null)
            throw new NotFoundException<Report>();

        if (report.CommmentId.HasValue)
        {
            var comment = await _comRepo.GetByIdAsync(report.CommmentId.Value, x => x, false, false);
            if (comment == null)
                throw new NotFoundException<Comment>();
            _repo.Delete(report);
            await _comRepo.DeleteAsync(report.CommmentId.Value);
            await _comRepo.SaveAsync();
        }

        report.IsResolved = true;
        await _repo.SaveAsync();
    }

}
