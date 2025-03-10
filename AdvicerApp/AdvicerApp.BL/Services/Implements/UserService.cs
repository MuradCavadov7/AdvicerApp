using AdvicerApp.BL.DTOs.UserDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdvicerApp.BL.Services.Implements;

public class UserService(UserManager<User> _userManager,IReportRepository _reportRepo) : IUserService
{
    public async Task DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) throw new NotFoundException<User>();
        var result = await _userManager.DeleteAsync(user);
        string desc = "";
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                desc += (" " + error.Description);
            }
            throw new BadRequestException(desc);
        }

    }

    public async Task<IEnumerable<UserGetDto>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var userDtos = new List<UserGetDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Fullname = user.Fullname,
                Gender = user.Gender,
                Role = roles.FirstOrDefault()! 
            };
            userDtos.Add(userDto);
        }

        return userDtos;
    }


    public async Task<UserGetDto> GetByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new NotFoundException<User>();

        var roles = await _userManager.GetRolesAsync(user);
        return new UserGetDto
        {
            Id = user.Id,
            Username = user.UserName,
            Fullname = user.Fullname,
            Email = user.Email,
            Role = roles.FirstOrDefault()!,
            Gender = user.Gender,
        };
    }

    public async Task<bool> IsUserBanned(string userId)
    {
        return await _reportRepo.IsExistAsync(x => x.Commment.UserId == userId && x.IsResolved);
    }
}
