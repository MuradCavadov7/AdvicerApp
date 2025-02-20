using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Enums;
using AdvicerApp.Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace AdvicerApp.BL.Services.Implements;

public class CommentService(ICommentRepository _repo, ICurrentUser _user, IRestaurantRepository _restRepo, IMapper _mapper,IUserService _userService,IWebHostEnvironment _env) : ICommentService
{
    private string _userId = _user.GetId();
    private string _userRole = _user.GetRole();
    public async Task<int> CreateAsync(CommentCreateDto dto)
    {
        if (!await _restRepo.IsExistAsync(dto.RestaurantId)) throw new NotFoundException<Restaurant>();

        if (dto.EvidentialImage != null)
        {
            if (!dto.EvidentialImage.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.EvidentialImage.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }
        var comment = _mapper.Map<Comment>(dto);

        if (dto.EvidentialImage != null)
        {
            comment.CommentImage = await dto.EvidentialImage.UploadAsync(_env.WebRootPath, "imgs", "comment");
        }
        else
        {
            comment.CommentImage = null;
        }

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


        if (await _userService.IsUserBanned(_user.GetId()))
        {
            throw new UnauthorizedAccessException("You are banned from commenting.");
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

        string imagePath = Path.Combine(_env.WebRootPath, "imgs", "comment", comment.CommentImage);
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

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
            CommentImage = x.CommentImage,
            Replies = x.Children.Select(r => new CommentGetDto
            {
                Id = r.Id,
                Text = r.Text,
                Username = r.User.UserName,
                ParentId = r.ParentId,
                CommentImage = r.CommentImage

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
            Children = x.Children,
            CommentImage = x.CommentImage
        }, false, false);

        if (comment == null) throw new NotFoundException<Comment>();

        if (dto.EvidentialImage != null)
        {
            if (!dto.EvidentialImage.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.EvidentialImage.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }

        string imagePath = Path.Combine(_env.WebRootPath, "imgs", "comment", comment.CommentImage);
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        if (dto.EvidentialImage != null)
        {
            comment.CommentImage = await dto.EvidentialImage.UploadAsync(_env.WebRootPath, "imgs", "comment");
        }
        else
        {
            comment.CommentImage = null;
        }

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

        if (await _userService.IsUserBanned(_user.GetId()))
        {
            throw new UnauthorizedAccessException("You are banned from commenting.");
        }

        _mapper.Map(dto, comment);

        comment.UpdatedTime = DateTime.UtcNow;

        await _repo.AddAsync(comment);

        await _repo.SaveAsync();
    }
}
