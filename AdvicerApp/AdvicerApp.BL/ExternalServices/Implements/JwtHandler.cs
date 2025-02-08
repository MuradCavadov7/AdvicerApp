using AdvicerApp.BL.DTOs.Options;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvicerApp.BL.ExternalServices.Implements;

public class JwtHandler : IJwtHandler
{
    readonly JwtOptions _opt;
    readonly UserManager<User> _userManager;
    public JwtHandler(IOptions<JwtOptions> opt, UserManager<User> userManager)
    {
        _opt = opt.Value;
        _userManager = userManager;
    }
    public async  Task<string> CreateJwtToken(User user, int hours)
    {
        var roles = await _userManager.GetRolesAsync(user);
        List<Claim> claims = [
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.GivenName,user.Fullname),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id)
            ];
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
        SigningCredentials credentials = new(key,SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims : claims,
            notBefore : DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(hours),
            signingCredentials: credentials);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(securityToken);

    }
}
