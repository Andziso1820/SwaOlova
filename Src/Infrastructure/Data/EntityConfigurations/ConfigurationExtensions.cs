using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Common;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations;

internal static class ConfigurationExtensions
{
    public static void ConfigureAuditableProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity<Guid>
    {
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ModifiedBy).HasMaxLength(100);
    }
}
