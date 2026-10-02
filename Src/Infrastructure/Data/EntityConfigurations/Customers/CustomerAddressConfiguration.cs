using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Customer;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Customers;

public sealed class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.ToTable("CustomerAddresses");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.AddressLine1).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Village).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Landmark).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Latitude).HasPrecision(18, 8).IsRequired();
        builder.Property(x => x.Longitude).HasPrecision(18, 8).IsRequired();
        builder.Property(x => x.GatePhotoUrl).HasMaxLength(500);
        builder.Property(x => x.IsDefault).IsRequired();

        builder.HasIndex(x => new { x.CustomerId, x.IsDefault });
    }
}
