namespace AdvicerApp.Core.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; set; }  
    public decimal Price { get; set; }
    public string Description { get; set; } 
    public string Image { get; set; }
    public int MenuId { get; set; }
    public Menu Menu { get; set; }
}
