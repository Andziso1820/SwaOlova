using SwaOlova.Domain.Notifications;

namespace SwaOlova.Application.Features.Notifications.Dtos;

public static class NotificationDtoMapper
{
    public static NotificationDto ToDto(Notification notification)
    {
        return new NotificationDto(
            notification.Id,
            notification.Recipient,
            notification.Message,
            notification.Channel,
            notification.Sent,
            null); // SentAt would need to be added to domain entity if tracking is needed
    }
}
