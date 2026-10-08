using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Rider;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Riders;

public sealed class RiderActivityConfiguration : IEntityTypeConfiguration<RiderActivity>
{
    public void Configure(EntityTypeBuilder<RiderActivity> builder)
    {
        builder.ToTable("RiderActivities");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.RiderId).IsRequired();
        builder.Property(x => x.ActivityType).HasConversion<string>().HasMaxLength(100).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();

        builder.HasIndex(x => x.RiderId);
        builder.HasIndex(x => new { x.RiderId, x.CreatedDate });

        builder.HasOne<Rider>()
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.RiderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
