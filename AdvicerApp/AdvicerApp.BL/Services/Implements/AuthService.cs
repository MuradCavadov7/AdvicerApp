using AdvicerApp.BL.DTOs.UserDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdvicerApp.BL.Services.Implements;

public class AuthService(UserManager<User> _userManager, IJwtHandler _jwtHandler,RoleManager<IdentityRole> _roleManager,SignInManager<User> _signInManager) : IAuthService
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
        List<string> desc = new();
        if(!result.Succeeded)
        {
            foreach(var error in  result.Errors)
            {
                desc.Add(error.Description);
            }
        }
        if(!dto.IsRestaurantOwner)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Role.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    desc.Add(error.Description);
                }
            }
        }
        else
        {
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Role.Owner));
            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    desc.Add(error.Description);
                }
            }
        }
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
        if(dto.UsernameOrEmail.Contains('@'))
        {
            user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);
        }
        else
        {
            user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);
        }
        if (user == null) throw new BadRequestException("Username or password is(are) wrong");
        List<string> desc = new();

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password,false);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut) throw new UserLockedOutException();
            if(result.IsNotAllowed) throw new BadRequestException("Username or password is(are) wrong");
        } 
        return _jwtHandler.CreateJwtToken(user, 36);
    }

}
