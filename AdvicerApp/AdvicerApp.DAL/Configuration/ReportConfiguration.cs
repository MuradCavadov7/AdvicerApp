using AdvicerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reason)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Reports)
            .HasForeignKey(x => x.OwnerId);

        builder.HasOne(x => x.Commment)
            .WithMany(x => x.Reports)
            .HasForeignKey(x => x.CommmentId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
