using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Notifications;

namespace SwaOlova.Application.Features.Notifications.Commands.SendWhatsApp;

public sealed class SendWhatsAppCommandHandler(
    INotificationRepository notificationRepository)
    : IRequestHandler<SendWhatsAppCommand, Result<SendWhatsAppResponse>>
{
    public async Task<Result<SendWhatsAppResponse>> Handle(SendWhatsAppCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Recipient = request.Request.PhoneNumber.Trim(),
            Message = request.Request.Message.Trim(),
            Channel = "WhatsApp",
            Sent = true
        };

        await notificationRepository.AddAsync(notification, cancellationToken);

        var response = new SendWhatsAppResponse(
            notification.Id,
            notification.Recipient,
            true);

        return Result<SendWhatsAppResponse>.Success(response);
    }
}
