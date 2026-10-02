namespace SwaOlova.Application.Features.Notifications.Commands.SendSms;

public sealed record SendSmsRequest(
    string PhoneNumber,
    string Message);

public sealed record SendSmsResponse(
    Guid NotificationId,
    string PhoneNumber,
    bool Success);
