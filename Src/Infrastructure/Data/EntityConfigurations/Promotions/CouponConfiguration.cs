using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Promotions;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Promotions;

public sealed class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
        builder.Property(x => x.DiscountAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.ExpiryDate).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.ExpiryDate);
    }
}
