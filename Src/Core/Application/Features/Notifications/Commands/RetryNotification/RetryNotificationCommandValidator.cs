using FluentValidation;

namespace SwaOlova.Application.Features.Notifications.Commands.RetryNotification;

public sealed class RetryNotificationCommandValidator : AbstractValidator<RetryNotificationCommand>
{
    public RetryNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId).NotEmpty();
    }
}
