using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.Helper;
using AdvicerApp.BL.Services.Implements;
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

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm] CreateRestaurantDto dto)
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
        public async Task<IActionResult> Update(int id, [FromForm] UpdateResturantDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterRestaurants([FromQuery] string? category, [FromQuery] string? address, [FromQuery] string? name)
        {
            var restaurants = await _service.GetFilteredRestaurantsAsync(category, address, name);
            return Ok(restaurants);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{restaurantId}")]
        public async Task<IActionResult> DeleteImage(int restaurantId, ICollection<int> imgIds)
        {
            await _service.DeleteImagesAsync(restaurantId, imgIds);
            return Ok();
        }
    }
}
