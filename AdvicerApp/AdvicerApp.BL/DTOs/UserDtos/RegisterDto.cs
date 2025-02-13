using AdvicerApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.DTOs.UserDtos;

public class RegisterDto
{
    public string Fullname {  get; set; }
    public string Username {  get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RePassword {  get; set; }
    public Gender Gender { get; set; }
    
    public bool IsRestaurantOwner {  get; set; }
    public IFormFile OwnerDocument { get; set; }
}
