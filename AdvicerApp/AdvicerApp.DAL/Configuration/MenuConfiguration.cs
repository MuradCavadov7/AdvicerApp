using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Menus)
            .HasForeignKey(x => x.RestaurantId);
    }
}
