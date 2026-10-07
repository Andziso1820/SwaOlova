using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Notifications.Commands.RetryNotification;
using SwaOlova.Application.Features.Notifications.Commands.SendEmail;
using SwaOlova.Application.Features.Notifications.Commands.SendSms;
using SwaOlova.Application.Features.Notifications.Commands.SendWhatsApp;
using SwaOlova.Application.Features.Notifications.Dtos;

namespace SwaOlova.Application.Features.Notifications.Services;

public interface INotificationOrchestrator
{
    Task<Result<SendWhatsAppResponse>> SendWhatsAppAsync(SendWhatsAppRequest request, CancellationToken cancellationToken = default);
    Task<Result<SendSmsResponse>> SendSmsAsync(SendSmsRequest request, CancellationToken cancellationToken = default);
    Task<Result<SendEmailResponse>> SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken = default);
    Task<Result> RetryNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default);
}
