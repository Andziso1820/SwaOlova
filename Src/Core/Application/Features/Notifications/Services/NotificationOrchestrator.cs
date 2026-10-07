using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Notifications.Commands.RetryNotification;
using SwaOlova.Application.Features.Notifications.Commands.SendEmail;
using SwaOlova.Application.Features.Notifications.Commands.SendSms;
using SwaOlova.Application.Features.Notifications.Commands.SendWhatsApp;
using SwaOlova.Application.Features.Notifications.Dtos;

namespace SwaOlova.Application.Features.Notifications.Services;

public sealed class NotificationOrchestrator(IMediator mediator)
    : INotificationOrchestrator
{
    public async Task<Result<SendWhatsAppResponse>> SendWhatsAppAsync(SendWhatsAppRequest request, CancellationToken cancellationToken = default)
    {
        var command = new SendWhatsAppCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<SendSmsResponse>> SendSmsAsync(SendSmsRequest request, CancellationToken cancellationToken = default)
    {
        var command = new SendSmsCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<SendEmailResponse>> SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken = default)
    {
        var command = new SendEmailCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result> RetryNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default)
    {
        var command = new RetryNotificationCommand(notificationId);
        return await mediator.Send(command, cancellationToken);
    }
}
