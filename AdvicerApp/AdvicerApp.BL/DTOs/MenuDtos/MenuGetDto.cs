using AdvicerApp.BL.DTOs.MenuItemDtos;

namespace AdvicerApp.BL.DTOs.MenuDtos;

public class MenuGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int RestaurantId {  get; set; }
    public ICollection<MenuItemGetDto> MenuItems { get; set; }
}
