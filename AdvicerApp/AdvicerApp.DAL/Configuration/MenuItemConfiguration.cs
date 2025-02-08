using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Image)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Menu)
            .WithMany(x => x.MenuItems)
            .HasForeignKey(x => x.MenuId);
    }
}
