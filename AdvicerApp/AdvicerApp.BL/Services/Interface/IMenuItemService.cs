using AdvicerApp.BL.DTOs.MenuItemDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IMenuItemService
{
    Task<int> CreateAsync(MenuItemCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id,MenuItemUpdateDto dto);
    Task<IEnumerable<MenuItemGetDto>> GetAllAsync();
    Task<MenuItemGetDto> GetByIdAsync(int id);
}
