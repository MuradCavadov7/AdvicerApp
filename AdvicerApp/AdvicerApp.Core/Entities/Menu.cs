namespace AdvicerApp.Core.Entities;

public class Menu : BaseEntity
{
    public string Name { get; set; } 
    public string Description { get; set; } 
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; }
}
