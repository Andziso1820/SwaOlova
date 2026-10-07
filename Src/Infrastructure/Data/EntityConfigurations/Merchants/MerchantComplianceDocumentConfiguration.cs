using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Merchants;

public sealed class MerchantComplianceDocumentConfiguration : IEntityTypeConfiguration<MerchantComplianceDocument>
{
    public void Configure(EntityTypeBuilder<MerchantComplianceDocument> builder)
    {
        builder.ToTable("MerchantComplianceDocuments");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.MerchantId).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
        builder.Property(x => x.DocumentType)
            .HasMaxLength(50)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(x => x.FileUrl).HasMaxLength(500).IsRequired();
        builder.Property(x => x.StoredFileName).HasMaxLength(260);
        builder.Property(x => x.ContentType).HasMaxLength(100);
        builder.Property(x => x.FileSize);
        builder.Property(x => x.FileData).HasColumnType("varbinary(max)");
        builder.Property(x => x.ExpiryDate);

        builder.HasIndex(x => x.MerchantId);
    }
}
