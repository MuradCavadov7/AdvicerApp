using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class RestaurantImageConfiguration : IEntityTypeConfiguration<RestaurantImage>
{
    public void Configure(EntityTypeBuilder<RestaurantImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.RestaurantImages)
            .HasForeignKey(x => x.RestaurantId);
    }
}
