using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;

namespace AdvicerApp.DAL.Repositories;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    public StatusRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
}
