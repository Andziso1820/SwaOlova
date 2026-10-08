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
        builder.Property(x => x.DocumentType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.StoredFileName).HasMaxLength(260);
        builder.Property(x => x.ContentType).HasMaxLength(100);
        builder.Property(x => x.FileSize);
        builder.Property(x => x.FileData).HasColumnType("varbinary(max)");
        builder.Property(x => x.ExpiryDate);

        builder.HasIndex(x => x.RiderId);

        builder.HasOne<Rider>()
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.RiderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
