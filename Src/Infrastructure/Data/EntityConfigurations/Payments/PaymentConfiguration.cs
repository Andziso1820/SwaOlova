using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Order;
using SwaOlova.Domain.Payment;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Payments;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.PaymentMethod).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(x => x.ExternalReference).HasMaxLength(200);

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.ExternalReference);

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
