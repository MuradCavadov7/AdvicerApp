using System.ComponentModel.DataAnnotations;

namespace AdvicerApp.Core.Entities;

public class Rating : BaseEntity
{
    [Range(1,5)]
    public int Score {  get; set; }
    public string UserId {  get; set; }
    public User User { get; set; }
    public int RestaurantId {  get; set; }
    public Restaurant Restaurant { get; set; }
}
