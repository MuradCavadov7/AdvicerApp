using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;

namespace AdvicerApp.DAL.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
}
