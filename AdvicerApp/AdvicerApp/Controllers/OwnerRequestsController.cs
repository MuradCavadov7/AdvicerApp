using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdvicerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnerRequestsController(IOwnerApproveService _service) : ControllerBase
{
    [HttpPost("request")]
    public async Task<IActionResult> RequestApproval([FromForm] IFormFile document)
    {
        await _service.RequestApprovalAsync(document);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("approve/{userId}")]
    public async Task<IActionResult> ApproveOwner(string userId)
    {
        await _service.ApproveOwnerAsync(userId);
        return Ok();
    }
}
