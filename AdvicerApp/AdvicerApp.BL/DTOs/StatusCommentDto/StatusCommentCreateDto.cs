using System.Text.Json.Serialization;

namespace AdvicerApp.BL.DTOs.StatusCommentDto;

public class StatusCommentCreateDto
{
    public string Content { get; set; }
    public int StatusId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ParentId { get; set; }
}
