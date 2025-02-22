namespace AdvicerApp.Core.Entities;

public class Status : BaseEntity
{
    public string Content { get; set; }
    public string UserId { get; set; }
    public  User User { get; set; }
    public  ICollection<StatusComment> StatusComments { get; set; }
}
