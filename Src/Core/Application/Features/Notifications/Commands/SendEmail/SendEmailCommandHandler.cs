using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Notifications;

namespace SwaOlova.Application.Features.Notifications.Commands.SendEmail;

public sealed class SendEmailCommandHandler(
    INotificationRepository notificationRepository)
    : IRequestHandler<SendEmailCommand, Result<SendEmailResponse>>
{
    public async Task<Result<SendEmailResponse>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Recipient = request.Request.EmailAddress.Trim(),
            Message = $"{request.Request.Subject}: {request.Request.Message}",
            Channel = "Email",
            Sent = true
        };

        await notificationRepository.AddAsync(notification, cancellationToken);

        var response = new SendEmailResponse(
            notification.Id,
            notification.Recipient,
            true);

        return Result<SendEmailResponse>.Success(response);
    }
}
