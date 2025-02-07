using AdvicerApp.BL.DTOs.RestaurantDtos;

namespace AdvicerApp.BL.DTOs.CategoryDtos;

public class CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<RestaurantGetDto>? Restaurants { get; set; }
}
