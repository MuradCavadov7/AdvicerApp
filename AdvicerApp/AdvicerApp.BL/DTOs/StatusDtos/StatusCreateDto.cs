using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.StatusDtos;

public class StatusCreateDto
{
    public string Content { get; set; }
    public IFormFile? StatusImage { get; set; }
    
}
