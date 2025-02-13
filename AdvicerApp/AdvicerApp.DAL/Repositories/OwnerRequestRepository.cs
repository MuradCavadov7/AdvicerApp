using AdvicerApp.Core.Entities.Common;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;

namespace AdvicerApp.DAL.Repositories;

public class OwnerRequestRepository : GenericRepository<OwnerRequest>, IOwnerRequestRepository
{
    public OwnerRequestRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
}
