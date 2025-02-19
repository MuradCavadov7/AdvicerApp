using AdvicerApp.Core.Enums;

namespace AdvicerApp.BL.DTOs.UserDtos;

public class UserGetDto
{
    public string Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Role { get; set; }
}
