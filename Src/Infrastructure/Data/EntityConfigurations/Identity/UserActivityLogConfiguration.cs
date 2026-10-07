using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Identity;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Identity;

public sealed class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
{
    public void Configure(EntityTypeBuilder<UserActivityLog> builder)
    {
        builder.ToTable("UserActivityLogs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Action)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.EntityName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.EntityId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ActivityDate)
            .IsRequired();

        builder.Property(x => x.IpAddress)
            .HasMaxLength(45);

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.Property(x => x.Details)
            .HasColumnType("nvarchar(max)");

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.ActivityDate);

        builder.HasIndex(x => x.EntityName);

        builder.HasIndex(x => new { x.UserId, x.ActivityDate })
            .IsDescending(false, true);
    }
}
