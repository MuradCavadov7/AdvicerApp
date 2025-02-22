using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.StatusDtos;

public class StatusUpdateDto
{
    public string Content { get; set; }
    public IFormFile? StatusImage { get; set; }
}
