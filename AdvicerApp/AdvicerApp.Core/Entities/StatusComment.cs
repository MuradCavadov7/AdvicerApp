namespace AdvicerApp.Core.Entities;

 public class StatusComment : BaseEntity
{
    public string Content { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; }
    public int? ParentId { get; set; }
    public StatusComment Parent { get; set; }
    public ICollection<StatusComment> Children { get; set; }
}
