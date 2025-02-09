using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.MenuItemDtos;

public class MenuItemUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile? File { get; set; }
}
