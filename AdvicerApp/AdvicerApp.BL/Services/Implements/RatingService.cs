using AdvicerApp.BL.DTOs.RatingDto;
using AdvicerApp.BL.DTOs.RatingDtos;
using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Enums;
using AdvicerApp.Core.Repositories;

namespace AdvicerApp.BL.Services.Implements;

public class RatingService(IRatingRepository _repo, IRestaurantRepository _restRepo, ICurrentUser _user) : IRatingService
{
    private string _userId = _user.GetId();
    private string _userRole = _user.GetRole();

    public async Task<int> CreateAsync(RatingCreateDto dto)
    {
        var restaurant = await _restRepo.GetByIdAsync(dto.RestaurantId, x => new RestaurantGetDto { Id = x.Id }, false, false);

        if (restaurant == null) throw new NotFoundException<Restaurant>();
        if(_userRole != nameof(Role.User)) throw new UnauthorizedAccessException("Only User can rate");

        if (dto.Score < 1 && dto.Score > 5) throw new InvalidScoreException("Rating score must be between 1-5");

        var rating = new Rating();
        rating.Score = dto.Score;
        rating.UserId = _userId;
        rating.RestaurantId = dto.RestaurantId;
        await _repo.AddAsync(rating);
        await _repo.SaveAsync();
        return rating.Id;
    }

    public async Task DeleteAsync(int ratingId)
    {
        var rating  = await _repo.GetByIdAsync(ratingId,x=>new Rating { Id = x.Id },false,false);
        if (rating == null) throw new NotFoundException<Rating>();
        _repo.Delete(rating);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int ratingId, RatingUpdateDto dto)
    {
        var rating = await _repo.GetByIdAsync(ratingId, x => new Rating { Id = x.Id,Score = x.Score }, false, false);
        if (rating == null) throw new NotFoundException<Rating>();
        if (_userRole != nameof(Role.User)) throw new UnauthorizedAccessException("Only User can change rating");
        rating.Score = dto.Score;
        await _repo.SaveAsync();
    }
}
