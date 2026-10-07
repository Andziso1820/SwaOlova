using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwaOlova.Domain.Notifications;
using SwaOlova.Infrastructure.Data.EntityConfigurations;

namespace SwaOlova.Infrastructure.Data.EntityConfigurations.Notifications;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");
        builder.HasKey(x => x.Id);
        builder.ConfigureAuditableProperties();

        builder.Property(x => x.Recipient).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Message).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.Channel).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Sent).IsRequired();

        builder.HasIndex(x => x.Recipient);
        builder.HasIndex(x => x.Channel);
        builder.HasIndex(x => x.Sent);
    }
}
