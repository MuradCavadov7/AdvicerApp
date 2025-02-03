namespace AdvicerApp.Core.Entities;

public class Menu : BaseEntity
{
    public string Name {  get; set; }
    public decimal Price {  get; set; }
    public string Description { get; set; }
    public string Image {  get; set; }
    public int RestaurantId {  get; set; }
    public Restaurant Restaurant { get; set; }
}
