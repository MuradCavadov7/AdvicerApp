using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdvicerApp.DAL.Contexts;

public class AdvicerAppDbContext : IdentityDbContext<User>
{

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RestaurantImage> RestaurantImages {  get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<OwnerRequest> OwnerRequests { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<StatusComment> StatusComments { get; set; }
    
    public AdvicerAppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AdvicerAppDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
