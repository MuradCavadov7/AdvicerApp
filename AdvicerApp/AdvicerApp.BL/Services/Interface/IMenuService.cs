using AdvicerApp.BL.DTOs.MenuDtos;
using AdvicerApp.BL.DTOs.MenuItemDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IMenuService
{
    Task<int> CreateAsync(MenuCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, MenuUpdateDto dto);
    Task<IEnumerable<MenuGetDto>> GetAllAsync();
    Task<MenuGetDto> GetByIdAsync(int id);
}
