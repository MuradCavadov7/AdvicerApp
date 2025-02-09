﻿using AdvicerApp.BL.DTOs.MenuItemDtos;
using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController(IMenuItemService _service) : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm] MenuItemCreateDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }


        [Authorize(Roles = "Owner")]
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


        [Authorize(Roles = "Owner")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] MenuItemUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
        }
    }
}

