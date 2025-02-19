using AdvicerApp.BL.DTOs.ReportDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IReportService
{
    Task<int> CreateAsync(ReportCreateDto dto);
    Task<IEnumerable<ReportGetDto>> GetAllAsync();
    Task<ReportGetDto> GetByIdAsync(int id);
    Task ResolveAsync(int id);
}
