using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Rider;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Riders;

public sealed class RiderDocumentConfiguration : IEntityTypeConfiguration<RiderDocument>
{
    public void Configure(EntityTypeBuilder<RiderDocument> builder)
    {
        builder.ToTable("RiderDocuments");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.RiderId).IsRequired();
        builder.Property(x => x.FileName).HasMaxLength(250).IsRequired();
        builder.Property(x => x.FileUrl).HasMaxLength(500).IsRequired();

        builder.HasIndex(x => x.RiderId);
    }
}
