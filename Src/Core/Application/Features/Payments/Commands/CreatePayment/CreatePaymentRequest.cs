namespace SwaOlova.Application.Features.Payments.Commands.CreatePayment;

public sealed record CreatePaymentRequest(
    Guid OrderId,
    decimal Amount,
    string PaymentMethod);
