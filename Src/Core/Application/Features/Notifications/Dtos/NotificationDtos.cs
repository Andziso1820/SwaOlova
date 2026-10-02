namespace SwaOlova.Application.Features.Notifications.Dtos;

public sealed record NotificationDto(
    Guid Id,
    string Recipient,
    string Message,
    string Channel,
    bool Sent,
    DateTime? SentAt);

public sealed record NotificationSummaryDto(
    int TotalSent,
    int TotalFailed,
    int Pending);
