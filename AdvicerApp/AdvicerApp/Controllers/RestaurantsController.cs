using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.Helper;
using AdvicerApp.BL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvicerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RestaurantsController(IRestaurantService _service) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [Authorize(Roles = RoleConstants.AccessToRestaurant)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm]CreateRestaurantDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }


        [Authorize(Roles = RoleConstants.AccessToRestaurant)]
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


        [Authorize(Roles = RoleConstants.AccessToRestaurant)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateResturantDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
        }
    }
}
