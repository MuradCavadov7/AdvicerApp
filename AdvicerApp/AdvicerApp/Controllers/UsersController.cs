using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService _service) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _service.GetAllAsync();
        return Ok(users);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetById(string id)
    {

        return Ok(await _service.GetByIdAsync(id));
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {

        await _service.DeleteAsync(id);
        return NoContent();
    }
}
