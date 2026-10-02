using SwaOlova.Application.Features.Payments.Dtos;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentsByOrder;

public sealed record GetPaymentsByOrderResponse(
    IReadOnlyCollection<PaymentDto> Payments,
    decimal TotalAmount);
