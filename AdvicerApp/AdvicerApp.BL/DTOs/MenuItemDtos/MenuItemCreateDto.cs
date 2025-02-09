using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.MenuItemDtos;

public class MenuItemCreateDto
{
    public string Name {  get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile File { get; set; }
    public int MenuId {  get; set; }

}
