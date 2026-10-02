using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Merchants;

public sealed class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
{
    public void Configure(EntityTypeBuilder<Merchant> builder)
    {
        builder.ToTable("Merchants");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.MerchantCode).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ContactNumber).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(x => x.MerchantCategoryId).IsRequired();

        builder.HasIndex(x => x.MerchantCode).IsUnique();
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Status);

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.AddressLine1).HasMaxLength(250).IsRequired();
            address.Property(x => x.Village).HasMaxLength(150).IsRequired();
            address.Property(x => x.Latitude).HasPrecision(18, 8).IsRequired();
            address.Property(x => x.Longitude).HasPrecision(18, 8).IsRequired();
        });

        builder.HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ComplianceDocuments)
            .WithOne()
            .HasForeignKey(x => x.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
