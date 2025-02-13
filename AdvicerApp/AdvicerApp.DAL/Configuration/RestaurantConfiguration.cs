using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x=>x.Description)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x=>x.Image)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(x => x.Owner)
            .WithMany(x=>x.Restaurants)
            .HasForeignKey(x => x.OwnerId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Restaurants)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
