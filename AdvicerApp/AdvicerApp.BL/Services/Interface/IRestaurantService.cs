using AdvicerApp.BL.DTOs.RestaurantDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IRestaurantService
{
    Task<int> CreateAsync(CreateRestaurantDto dto);
    Task UpdateAsync(int id,UpdateResturantDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<RestaurantGetDto>> GetAllAsync();
    Task<RestaurantGetDto> GetByIdAsync(int id);
}
