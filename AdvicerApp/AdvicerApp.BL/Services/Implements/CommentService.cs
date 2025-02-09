using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AutoMapper;
using System.Xml.Linq;

namespace AdvicerApp.BL.Services.Implements;

public class CommentService(ICommentRepository _repo,ICurrentUser _user,IRestaurantRepository _restRepo,IMapper _mapper) : ICommentService
{
    private string _userId = _user.GetId();
    public async Task<int> CreateAsync(CommentCreateDto dto)
    {
        if (!await _restRepo.IsExistAsync(dto.RestaurantId)) throw new NotFoundException<Restaurant>();
        var comment = _mapper.Map<Comment>(dto);
        comment.UserId = _userId;
        Comment? parent = null;
        if (dto.ParentId.HasValue)
        {
            parent = await _repo.GetByIdAsync(dto.ParentId.Value,x=>x.Parent,false,false);
            if (parent is null)
                throw new NotFoundException<Comment>();
        }
        comment.RestaurantId = parent?.RestaurantId ?? dto.RestaurantId;
        await _repo.AddAsync(comment);
        await _repo.SaveAsync();
        return comment.Id;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CommentGetDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CommentGetDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, CommentUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
