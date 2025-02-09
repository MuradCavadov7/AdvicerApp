namespace AdvicerApp.BL.DTOs.MenuItemDtos;

public class MenuItemGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int MenuId { get; set; }
}
