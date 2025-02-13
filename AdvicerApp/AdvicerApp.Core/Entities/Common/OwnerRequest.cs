namespace AdvicerApp.Core.Entities.Common;

public class OwnerRequest : BaseEntity
{
    public User Owner { get; set; }
    public string OwnerId { get; set; }
    public string DocumentUrl { get; set; }
    public bool IsApproved { get; set; } = false;
}
