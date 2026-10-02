using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Delivery;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Deliveries;

public sealed class ProofOfDeliveryConfiguration : IEntityTypeConfiguration<ProofOfDelivery>
{
    public void Configure(EntityTypeBuilder<ProofOfDelivery> builder)
    {
        builder.ToTable("ProofOfDeliveries");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.DeliveryId).IsRequired();
        builder.Property(x => x.OtpCode).HasMaxLength(20);
        builder.Property(x => x.PhotoUrl).HasMaxLength(500);
        builder.Property(x => x.SignatureUrl).HasMaxLength(500);

        builder.HasIndex(x => x.DeliveryId).IsUnique();
    }
}
