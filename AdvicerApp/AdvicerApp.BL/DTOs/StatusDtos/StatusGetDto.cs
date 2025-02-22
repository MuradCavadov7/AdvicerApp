using AdvicerApp.BL.DTOs.StatusCommentDto;
using AdvicerApp.Core.Entities;

namespace AdvicerApp.BL.DTOs.StatusDtos;

public class StatusGetDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string StatusImage { get; set; }
    public string UserId { get; set; }
    public ICollection<StatusCommentGetDto> StatusComments { get; set; }
}
