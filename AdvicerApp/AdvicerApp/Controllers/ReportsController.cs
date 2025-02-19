using AdvicerApp.BL.DTOs.ReportDtos;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController(IReportService _service) : ControllerBase
{
    [Authorize(Roles = "Owner")]
    [HttpPost]
    public async Task<IActionResult> CreateReport([FromForm] ReportCreateDto dto)
    {
        var reportId = await _service.CreateAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _service.GetAllAsync();
        return Ok(reports);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(int id)
    {
        var report = await _service.GetByIdAsync(id);
        return Ok(report);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("resolve/{id}")]
    public async Task<IActionResult> ResolveReport(int id)
    {
        await _service.ResolveAsync(id);
        return Ok();
    }
}
