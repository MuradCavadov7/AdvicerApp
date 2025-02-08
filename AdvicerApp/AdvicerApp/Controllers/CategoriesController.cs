using AdvicerApp.BL.DTOs.CategoryDtos;
using AdvicerApp.BL.Helper;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController(ICategoryService _service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCategory()
    {
        var cats = await _service.GetAllAsync();
        return Ok(cats);
    }


    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }



    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _service.DeleteAsync(id);
        return NoContent();

    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
    {

        await _service.UpdateAsync(id, dto);
        return Ok();
    }
}
