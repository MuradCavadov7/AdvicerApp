using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdvicerApp.DAL.Repositories;

public class RestaurantImagesRepository : GenericRepository<RestaurantImage>, IRestaurantImagesRepository
{
    public RestaurantImagesRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
        public async Task DeleteRangeAsync(IEnumerable<RestaurantImage> images)
        {
            Table.RemoveRange(images);
            await SaveAsync();
        }

}
