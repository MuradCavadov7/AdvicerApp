namespace AdvicerApp.Core.Entities;

public class Category : BaseEntity
{
    public string Name {  get; set; }
    public ICollection<Restaurant> Restaurants { get; set; }

}
