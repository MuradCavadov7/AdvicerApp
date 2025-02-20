namespace AdvicerApp.BL.DTOs.CommentDtos;

public class CommentGetDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Username { get; set; }
    public string? CommentImage { get; set; }
    public string Text { get; set; }
    public IEnumerable<CommentGetDto> Replies { get; set; }
}
