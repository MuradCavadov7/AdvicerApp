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
            new Claim("Username",user.UserName),
            new Claim("Fullname",user.Fullname),
            new Claim("Email",user.Email),
            new Claim("Role",user.Role.ToString()),
            new Claim("Id",user.Id)
            ];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
        SigningCredentials credentials = new(key,SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims : claims,
            notBefore : DateTime.Now,
            expires: DateTime.Now.AddMinutes(hours),
            signingCredentials: credentials);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(securityToken);

    }
}
