using AdvicerApp.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace AdvicerApp.Core.Entities;

public class User : IdentityUser
{
    public string Fullname {  get; set; }
    public Gender Gender { get; set; }
    public ICollection<Restaurant> Restaurants { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<OwnerRequest> OwnerRequests { get; set; }

}
