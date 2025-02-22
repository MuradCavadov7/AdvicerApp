using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Statuses)
            .HasForeignKey(x => x.UserId);
    }
}
