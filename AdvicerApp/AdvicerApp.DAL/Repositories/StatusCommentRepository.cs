using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;

namespace AdvicerApp.DAL.Repositories;

public class StatusCommentRepository : GenericRepository<StatusComment>, IStatusCommentRepository
{
    public StatusCommentRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
}
