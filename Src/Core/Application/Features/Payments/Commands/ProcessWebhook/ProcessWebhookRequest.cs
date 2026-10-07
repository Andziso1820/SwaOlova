namespace SwaOlova.Application.Features.Payments.Commands.ProcessWebhook;

public sealed record ProcessWebhookRequest(
    string ExternalReference,
    string Status,
    string? FailureReason = null);
