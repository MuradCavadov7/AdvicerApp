using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.CommentDtos;

public class CommentUpdateDto
{
    public string Text {  get; set; }
    public IFormFile? EvidentialImage { get; set; }
}
