using AdvicerApp.BL.DTOs.RatingDto;
using AdvicerApp.BL.DTOs.RatingDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IRatingService
{
    Task<int> CreateAsync(RatingCreateDto dto);
    Task UpdateAsync(int ratingId,RatingUpdateDto dto);
    Task DeleteAsync(int ratingId);
}
