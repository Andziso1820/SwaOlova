using SwaOlova.Application.Features.Payments.Dtos;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentRefunds;

public sealed record GetPaymentRefundsResponse(
    IReadOnlyCollection<RefundDto> Refunds,
    decimal TotalRefunded);
