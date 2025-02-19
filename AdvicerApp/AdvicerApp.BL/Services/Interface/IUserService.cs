using AdvicerApp.BL.DTOs.UserDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<UserGetDto>> GetAllAsync();
    Task<UserGetDto> GetByIdAsync(string userId);
    Task DeleteAsync(string userId);
    Task<bool> IsUserBanned(string userId);
}
