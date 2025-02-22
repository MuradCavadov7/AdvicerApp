using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.DTOs.StatusDtos;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController(IStatusService _service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromForm] StatusCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }


    [Authorize(Roles = "User")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _service.DeleteAsync(id);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetById(int id)
    {

        return Ok(await _service.GetByIdAsync(id));
    }


    [Authorize(Roles = "User")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] StatusUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }
}
