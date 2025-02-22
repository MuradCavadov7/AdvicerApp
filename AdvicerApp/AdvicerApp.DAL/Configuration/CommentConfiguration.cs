using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.CommentImage)
            .IsRequired(false)
            .HasMaxLength(512);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Restaurant)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.RestaurantId);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
