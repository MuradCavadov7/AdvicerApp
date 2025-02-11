﻿using AdvicerApp.BL.DTOs.UserDtos;

namespace AdvicerApp.BL.Services.Interface;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
    Task VerifyEmailAsync(string email, int code);
    Task<int> SendVerificationCodeAsync(string email);
    Task CRole();
}
