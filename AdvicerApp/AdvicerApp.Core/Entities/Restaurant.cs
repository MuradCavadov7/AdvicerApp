namespace AdvicerApp.Core.Entities;

public class Restaurant : BaseEntity
{
    public string Name {  get; set; }
    public string Description {  get; set; }
    public string Address {  get; set; }
    public string Phone {  get; set; }
    public string Image {  get; set; }
    public string OwnerId {  get; set; }
    public User Owner { get; set; }
    public int CategoryId {  get; set; }
    public Category Category { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Menu> Menus { get; set; }
    public ICollection<RestaurantImage> RestaurantImages { get; set; }
    public ICollection<Comment> Comments {  get; set; }

    



}
