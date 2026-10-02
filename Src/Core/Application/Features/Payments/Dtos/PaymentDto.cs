using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Payments.Dtos;

public sealed record RefundDto(
    Guid Id,
    Guid PaymentId,
    decimal Amount,
    string Reason);

public sealed record PaymentDto(
    Guid Id,
    Guid OrderId,
    string TransactionId,
    decimal Amount,
    string PaymentMethod,
    PaymentStatus Status,
    string? ExternalReference,
    string? ReferenceNumber,
    DateTime? CreatedDate,
    DateTime? ProcessedDate,
    string? ErrorMessage,
    IReadOnlyCollection<RefundDto> Refunds);
