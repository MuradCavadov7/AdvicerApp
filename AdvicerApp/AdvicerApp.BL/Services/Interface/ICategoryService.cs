using AdvicerApp.BL.DTOs.CategoryDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface ICategoryService
{
    Task<int> CreateAsync(CategoryCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id,CategoryUpdateDto dto);
    Task<IEnumerable<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(int id);
}
