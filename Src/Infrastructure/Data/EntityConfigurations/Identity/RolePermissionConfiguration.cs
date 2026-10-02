using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Identity;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Identity;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoleName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PermissionId)
            .IsRequired();

        builder.Property(x => x.AssignedDate)
            .IsRequired();

        // Configure relationship to Permission
        builder.HasOne(x => x.Permission)
            .WithMany()
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Composite unique index: role + permission combination
        builder.HasIndex(x => new { x.RoleName, x.PermissionId })
            .IsUnique();

        builder.HasIndex(x => x.RoleName);

        builder.HasIndex(x => x.PermissionId);
    }
}
