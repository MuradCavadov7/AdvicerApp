namespace AdvicerApp.Core.Entities;

public class Report : BaseEntity
{
    public string OwnerId { get; set; } 
    public User Owner { get; set; }
    public int? CommentId { get; set; } 
    public Comment? Comment { get; set; }
    public string Reason { get; set; } 
    public bool IsResolved { get; set; } = false;
}

