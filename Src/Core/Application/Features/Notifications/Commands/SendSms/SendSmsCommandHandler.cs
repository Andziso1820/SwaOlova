using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Notifications;

namespace SwaOlova.Application.Features.Notifications.Commands.SendSms;

public sealed class SendSmsCommandHandler(
    INotificationRepository notificationRepository)
    : IRequestHandler<SendSmsCommand, Result<SendSmsResponse>>
{
    public async Task<Result<SendSmsResponse>> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Recipient = request.Request.PhoneNumber.Trim(),
            Message = request.Request.Message.Trim(),
            Channel = "SMS",
            Sent = true
        };

        await notificationRepository.AddAsync(notification, cancellationToken);

        var response = new SendSmsResponse(
            notification.Id,
            notification.Recipient,
            true);

        return Result<SendSmsResponse>.Success(response);
    }
}
