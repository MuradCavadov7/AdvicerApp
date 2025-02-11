using AdvicerApp.BL.DTOs.CommentDtos;
using AdvicerApp.BL.DTOs.MenuDtos;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(ICommentService _service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromForm] CommentCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }


    [AllowAnonymous]
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


    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] CommentUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }
}
