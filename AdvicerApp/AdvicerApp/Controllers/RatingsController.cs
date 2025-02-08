using AdvicerApp.BL.DTOs.CategoryDtos;
using AdvicerApp.BL.DTOs.RatingDto;
using AdvicerApp.BL.DTOs.RatingDtos;
using AdvicerApp.BL.Helper;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingsController(IRatingService _service) : ControllerBase
{
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> Create(RatingCreateDto dto)
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


    [Authorize(Roles = "User")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RatingUpdateDto dto)
    {

        await _service.UpdateAsync(id, dto);
        return Ok();
    }
}
