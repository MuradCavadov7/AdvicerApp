using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class StatusCommentConfiguration : IEntityTypeConfiguration<StatusComment>
{
    public void Configure(EntityTypeBuilder<StatusComment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasOne(x => x.User)
            .WithMany(x => x.StatusComments)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Status)
            .WithMany(x => x.StatusComments)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
