using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvicerApp.Core.Repositories;

public interface IRestaurantImagesRepository : IGenericRepository<RestaurantImage>
{
    public Task DeleteRangeAsync(IEnumerable<RestaurantImage> images);

}
