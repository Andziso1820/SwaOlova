using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Rider;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Riders;

public sealed class RiderConfiguration : IEntityTypeConfiguration<Rider>
{
    public void Configure(EntityTypeBuilder<Rider> builder)
    {
        builder.ToTable("Riders");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.RiderNumber).HasMaxLength(50).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.Property(x => x.DriversLicenseNumber).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.StatusReason).HasMaxLength(1000);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50).IsRequired();

        builder.HasIndex(x => x.RiderNumber).IsUnique();
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
        builder.HasIndex(x => x.DriversLicenseNumber).IsUnique();
        builder.HasIndex(x => x.Status);
    }
}
