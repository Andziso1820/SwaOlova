using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Notifications.Commands.RetryNotification;

public sealed record RetryNotificationCommand(Guid NotificationId)
    : CommandBase<Unit>;
