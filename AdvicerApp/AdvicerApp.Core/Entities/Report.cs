namespace AdvicerApp.Core.Entities;

public class Report : BaseEntity
{
    public string OwnerId { get; set; } 
    public User Owner { get; set; }
    public int? CommmentId { get; set; } 
    public Comment? Commment { get; set; }
    public string Reason { get; set; } 
    public bool IsResolved { get; set; } = false;
}

