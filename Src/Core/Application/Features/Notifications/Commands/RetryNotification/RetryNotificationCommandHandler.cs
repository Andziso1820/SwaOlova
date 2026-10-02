using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Notifications.Commands.RetryNotification;

public sealed class RetryNotificationCommandHandler(
    INotificationRepository notificationRepository)
    : IRequestHandler<RetryNotificationCommand, Result>
{
    public async Task<Result> Handle(RetryNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await notificationRepository.GetByIdAsync(request.NotificationId, cancellationToken);

        if (notification is null)
        {
            return Result.Failure($"Notification with ID '{request.NotificationId}' was not found.");
        }

        // Attempt to resend
        notification.Sent = true;
        await notificationRepository.UpdateAsync(notification, cancellationToken);

        return Result.Success();
    }
}
