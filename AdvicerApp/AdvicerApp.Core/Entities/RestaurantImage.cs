namespace AdvicerApp.Core.Entities;

public class RestaurantImage : BaseEntity
{
    public string ImageUrl {  get; set; }
    public int RestaurantId {  get; set; }
    public Restaurant Restaurant { get; set; }
}
