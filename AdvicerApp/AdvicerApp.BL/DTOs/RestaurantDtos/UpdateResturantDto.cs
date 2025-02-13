using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.RestaurantDtos;

public class UpdateResturantDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public int CategoryId { get; set; }
    public IFormFile File { get; set; }
    public ICollection<IFormFile> Files { get; set; }
}

