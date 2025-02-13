using AdvicerApp.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvicerApp.DAL.Configuration;

public class OwnerRequestConfiguration : IEntityTypeConfiguration<OwnerRequest>
{
    public void Configure(EntityTypeBuilder<OwnerRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DocumentUrl)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.OwnerRequests)
            .HasForeignKey(x => x.OwnerId);
    }
}
