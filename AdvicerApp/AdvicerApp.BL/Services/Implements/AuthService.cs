using AdvicerApp.BL.DTOs.UserDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Text;

namespace AdvicerApp.BL.Services.Implements;

public class AuthService(UserManager<User> _userManager, IJwtHandler _jwtHandler, RoleManager<IdentityRole> _roleManager, SignInManager<User> _signInManager, ICacheService _redis, IEmailSend _sendEmail, IOwnerApproveService _ownerApproveService) : IAuthService
{
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null) throw new ExistsException<User>("This email already exists.");
        if (await _userManager.FindByNameAsync(dto.Username) != null) throw new ExistsException<User>("This username already exists.");

        User user = new User()
        {
            Email = dto.Email,
            UserName = dto.Username,
            Gender = dto.Gender,
            Fullname = dto.Fullname
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        string desc = "";
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                desc += (" " + error.Description);
            }
                throw new BadRequestException(desc);
        }
        if (!dto.IsRestaurantOwner)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Role.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    desc += (" " + error.Description);
                }
                throw new BadRequestException(desc);
            }
        }
        await SendVerificationCodeAsync(user.Email);

        return user.UserName;
    }

    public async Task CRole()
    {
        foreach (Role item in Enum.GetValues(typeof(Role)))
        {
            await _roleManager.CreateAsync(new IdentityRole(item.GetRole()));
        }
    }
    public async Task<string> LoginAsync(LoginDto dto)
    {
        User? user = null;
        if (dto.UsernameOrEmail.Contains('@'))
        {
            user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);
        }
        else
        {
            user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);
        }
        if (user == null) throw new BadRequestException("Username or password is(are) wrong");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut) throw new UserLockedOutException();
            if (result.IsNotAllowed) throw new BadRequestException("Username or password is(are) wrong");
        }
        return await _jwtHandler.CreateJwtToken(user, 36);
    }

    public async Task VerifyEmailAsync(string email, int code)
    {
        var cachedCode = await _redis.GetAsync<int?>(email);
        if (cachedCode == null)
            throw new BadRequestException("Verification code expired or not found");

        if (cachedCode != code)
            throw new BadRequestException("Verification code is incorrect.");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new NotFoundException<User>("User not found");

        user.EmailConfirmed = true;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.Description);
            }
            throw new BadRequestException(sb.ToString().Trim());
        }
        await _redis.RemoveAsync(email);
    }

    public async Task<int> SendVerificationCodeAsync(string email)
    {
        var existingCode = await _redis.GetAsync<int?>(email);
        if (existingCode != null)
            throw new BadRequestException("Email already sent");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new NotFoundException<User>("Email not found");

        Random random = new Random();
        int code = random.Next(100000, 999999);
        await _sendEmail.SendEmailAsync(email, user.UserName, code.ToString());
        await _redis.SetAsync(email, code, 5);
        return code;
    }
}


