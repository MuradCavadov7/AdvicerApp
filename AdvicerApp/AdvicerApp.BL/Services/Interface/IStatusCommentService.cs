
using AdvicerApp.BL.DTOs.StatusCommentDto;

namespace AdvicerApp.BL.Services.Interface;

public interface IStatusCommentService
{
    Task<int> CreateAsync(StatusCommentCreateDto dto);
    Task UpdateAsync(int id, StatusCommentUpdateDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<StatusCommentGetDto>> GetAllAsync();
    Task<StatusCommentGetDto> GetByIdAsync(int id);
}
