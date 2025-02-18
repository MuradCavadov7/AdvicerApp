using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AdvicerApp.Core.Entities;

public class Comment : BaseEntity
{
    public string Text {  get; set; }
    public string UserId {  get; set; }
    public User User { get; set; }
    public int RestaurantId {  get; set; }
    public Restaurant Restaurant { get; set; }
    public int ParentId {  get; set; }
    public Comment Parent { get; set; }
    public ICollection<Comment> Children { get; set; }
    public ICollection<Report> Reports { get; set; }
}
