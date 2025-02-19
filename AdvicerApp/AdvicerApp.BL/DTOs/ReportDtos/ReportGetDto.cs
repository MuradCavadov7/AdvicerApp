namespace AdvicerApp.BL.DTOs.ReportDtos;

public class ReportGetDto
{
    public int Id { get; set; }
    public string OwnerId { get; set; } 
    public string OwnerUsername { get; set; } 
    public int CommentId { get; set; }
    public string CommentText { get; set; } 
    public string Reason { get; set; }
    public bool IsResolved { get; set; }
}
