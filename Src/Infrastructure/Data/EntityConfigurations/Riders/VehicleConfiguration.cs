using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Rider;
using SwaOlova.Domain.Vehicle;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Riders;

public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.RiderId).IsRequired();
        builder.Property(x => x.RegistrationNumber).HasMaxLength(50).IsRequired();
        builder.Property(x => x.VehicleType).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Make).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Model).HasMaxLength(100).IsRequired();

        builder.HasIndex(x => x.RiderId).IsUnique();
        builder.HasIndex(x => x.RegistrationNumber).IsUnique();

        builder.HasOne<Rider>()
            .WithOne()
            .HasForeignKey<Vehicle>(x => x.RiderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
