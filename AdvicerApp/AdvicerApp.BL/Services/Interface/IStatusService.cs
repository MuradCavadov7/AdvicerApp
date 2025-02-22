
using AdvicerApp.BL.DTOs.StatusDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IStatusService
{
    Task<int> CreateAsync(StatusCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, StatusUpdateDto dto);
    Task<IEnumerable<StatusGetDto>> GetAllAsync();
    Task<StatusGetDto> GetByIdAsync(int id);
}
