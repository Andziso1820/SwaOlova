using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Locations;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Locations;

public sealed class CoverageAreaConfiguration : IEntityTypeConfiguration<CoverageArea>
{
    public void Configure(EntityTypeBuilder<CoverageArea> builder)
    {
        builder.ToTable("CoverageAreas");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.MerchantId).IsRequired();
        builder.Property(x => x.ZoneId).IsRequired();
        builder.Property(x => x.VillageId);
        builder.Property(x => x.DeliveryFee).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.DeliveryTimeMinutes).IsRequired();
        builder.Property(x => x.IsAvailable).IsRequired();

        builder.HasIndex(x => x.MerchantId);
        builder.HasIndex(x => x.ZoneId);
        builder.HasIndex(x => x.VillageId);
        builder.HasIndex(x => new { x.MerchantId, x.ZoneId, x.VillageId }).IsUnique();

        builder.HasOne<Zone>()
            .WithMany(x => x.CoverageAreas)
            .HasForeignKey(x => x.ZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Village>()
            .WithMany()
            .HasForeignKey(x => x.VillageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
