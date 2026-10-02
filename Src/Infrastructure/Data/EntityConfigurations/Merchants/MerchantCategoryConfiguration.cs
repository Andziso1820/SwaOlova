using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Merchants;

public sealed class MerchantCategoryConfiguration : IEntityTypeConfiguration<MerchantCategory>
{
    public void Configure(EntityTypeBuilder<MerchantCategory> builder)
    {
        builder.ToTable("MerchantCategories");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
