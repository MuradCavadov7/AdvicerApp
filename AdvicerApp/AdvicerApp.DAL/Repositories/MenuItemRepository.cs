using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;

namespace AdvicerApp.DAL.Repositories;

public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
{
    public MenuItemRepository(AdvicerAppDbContext _context) : base(_context)
    {
    }
}
