using AdvicerApp.BL.DTOs.CommentDtos;

namespace AdvicerApp.BL.DTOs.RestaurantDtos;

public class RestaurantGetDto
{
    public int Id { get; set; }
    public string OwnerId {  get; set; }
    public string Name {  get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public decimal AverageRating {  get; set; }
    public int CategoryId {  get; set; }
    public string CategoryName { get; set; }
    public string Image {  get; set; }
    public ICollection<string> Images {  get; set; }
    public ICollection<CommentGetDto> Comments { get; set; }
}
