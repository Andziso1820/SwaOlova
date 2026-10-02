using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Delivery;
using SwaOlova.Domain.Order;
using SwaOlova.Domain.Rider;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Deliveries;

public sealed class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Deliveries");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.RiderId).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(x => x.PickupTime);
        builder.Property(x => x.DeliveryTime);
        builder.Property(x => x.DistanceKm).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.EstimatedDurationMinutes).HasPrecision(18, 2).IsRequired();

        builder.HasIndex(x => x.OrderId).IsUnique();
        builder.HasIndex(x => x.RiderId);
        builder.HasIndex(x => x.Status);

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Rider>()
            .WithMany()
            .HasForeignKey(x => x.RiderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
