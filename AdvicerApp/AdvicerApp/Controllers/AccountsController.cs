using AdvicerApp.BL.DTOs.UserDtos;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController(IAuthService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromForm]RegisterDto dto)
    {
        await _service.RegisterAsync(dto);
        return Created();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole()
    {
        await _service.CRole();
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> SendVerificationCode(string email)
    {
        return Ok(await _service.SendVerificationCodeAsync(email));
    }
    [HttpPost]
    public async Task<IActionResult> VerifyEmail(string email, int code)
    {
        await _service.VerifyEmailAsync(email, code);
        return Ok("Email is confirmed");
    }

}
