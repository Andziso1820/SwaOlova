namespace SwaOlova.Application.Features.Notifications.Commands.SendWhatsApp;

public sealed record SendWhatsAppRequest(
    string PhoneNumber,
    string Message);

public sealed record SendWhatsAppResponse(
    Guid NotificationId,
    string PhoneNumber,
    bool Success);
