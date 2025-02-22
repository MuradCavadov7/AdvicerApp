using AdvicerApp.BL.DTOs.CommentDtos;

namespace AdvicerApp.BL.DTOs.StatusCommentDto;

public class StatusCommentGetDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Username { get; set; }
    public int? ParentId { get; set; }
    public int StatusId { get; set; } 
    public int? ParentCommentId { get; set; }
    public IEnumerable<StatusCommentGetDto> Replies { get; set; }
}
