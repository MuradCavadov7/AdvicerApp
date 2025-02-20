using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace AdvicerApp.BL.DTOs.CommentDtos;

public class CommentCreateDto
{
    public string Text {  get; set; }
    public int RestaurantId {  get; set; }
    public IFormFile? EvidentialImage { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ParentId {  get; set; }
}
