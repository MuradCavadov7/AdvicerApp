using AdvicerApp.BL.DTOs.Options;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvicerApp.BL.ExternalServices.Implements;

public class JwtHandler : IJwtHandler
{
    readonly JwtOptions _opt;
    public JwtHandler(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value;
    }
    public string CreateJwtToken(User user, int hours)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.GivenName,user.Fullname),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id)
            ];
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
