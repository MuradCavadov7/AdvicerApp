using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;

namespace AdvicerApp.DAL.Repositories;

public class MenuRepository : GenericRepository<Menu>, IMenuRepository
{
    public MenuRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
}
