using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Merchants;

public sealed class MerchantActivityConfiguration : IEntityTypeConfiguration<MerchantActivity>
{
    public void Configure(EntityTypeBuilder<MerchantActivity> builder)
    {
        builder.ToTable("MerchantActivities");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.MerchantId).IsRequired();
        builder.Property(x => x.ActivityType).HasConversion<string>().HasMaxLength(100).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();

        builder.HasIndex(x => x.MerchantId);
        builder.HasIndex(x => new { x.MerchantId, x.CreatedDate });

        builder.HasOne<Merchant>()
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.MerchantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
