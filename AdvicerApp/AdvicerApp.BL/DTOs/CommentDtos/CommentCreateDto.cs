namespace AdvicerApp.BL.DTOs.CommentDtos;

public class CommentCreateDto
{
    public string Text {  get; set; }
    public int RestaurantId {  get; set; }
    public int? ParentId {  get; set; }
}
