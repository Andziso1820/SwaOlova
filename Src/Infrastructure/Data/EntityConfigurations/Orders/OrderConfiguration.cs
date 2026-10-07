using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Order;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Orders;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.OrderNumber).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.MerchantId).IsRequired();
        builder.Property(x => x.DeliveryAddressId).IsRequired();
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.OrderType).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(x => x.SubTotal).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.DeliveryFee).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Total).HasPrecision(18, 2).IsRequired();

        builder.HasIndex(x => x.OrderNumber).IsUnique();
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.MerchantId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.OrderDate);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
