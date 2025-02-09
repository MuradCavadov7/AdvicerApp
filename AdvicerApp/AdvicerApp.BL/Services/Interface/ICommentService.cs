using AdvicerApp.BL.DTOs.CommentDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface ICommentService
{
    Task<int> CreateAsync(CommentCreateDto dto);
    Task UpdateAsync(int id, CommentUpdateDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<CommentGetDto>> GetAllAsync();
    Task<CommentGetDto> GetByIdAsync(int id);
}
