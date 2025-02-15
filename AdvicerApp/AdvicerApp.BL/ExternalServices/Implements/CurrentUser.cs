using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AdvicerApp.BL.ExternalServices.Implements;

public class CurrentUser(IHttpContextAccessor _httpContext) : ICurrentUser
{
    ClaimsPrincipal? User = _httpContext.HttpContext?.User;
    public string GetEmail()
    {
        var value = User?.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public string GetFullname()
    {
        var value = User?.FindFirst(x => x.Type == ClaimTypes.GivenName)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public string GetId()
    {
        var value = User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public string GetRole()
    {
        var value = User?.FindFirst(x=>x.Type == ClaimTypes.Role)?.Value;
        if(value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public string GetUserName()
    {
        var value = User?.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }
}
