namespace SwaOlova.Application.Features.Notifications.Commands.SendEmail;

public sealed record SendEmailRequest(
    string EmailAddress,
    string Subject,
    string Message);

public sealed record SendEmailResponse(
    Guid NotificationId,
    string EmailAddress,
    bool Success);
