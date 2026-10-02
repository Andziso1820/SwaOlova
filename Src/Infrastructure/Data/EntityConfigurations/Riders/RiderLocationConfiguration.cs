using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Rider;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Riders;

public sealed class RiderLocationConfiguration : IEntityTypeConfiguration<RiderLocation>
{
    public void Configure(EntityTypeBuilder<RiderLocation> builder)
    {
        builder.ToTable("RiderLocations");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.RiderId).IsRequired();
        builder.Property(x => x.Latitude).HasPrecision(18, 8).IsRequired();
        builder.Property(x => x.Longitude).HasPrecision(18, 8).IsRequired();
        builder.Property(x => x.RecordedAt).IsRequired();

        builder.HasIndex(x => x.RiderId);
        builder.HasIndex(x => x.RecordedAt);
    }
}
