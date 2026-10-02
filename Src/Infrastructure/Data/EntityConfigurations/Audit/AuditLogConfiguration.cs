using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Auditing;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Audit;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.EntityName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Action).HasMaxLength(100).IsRequired();
        builder.Property(x => x.UserName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.OldValues).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.NewValues).HasColumnType("nvarchar(max)").IsRequired();

        builder.HasIndex(x => x.EntityName);
        builder.HasIndex(x => x.Action);
        builder.HasIndex(x => x.UserName);
    }
}
