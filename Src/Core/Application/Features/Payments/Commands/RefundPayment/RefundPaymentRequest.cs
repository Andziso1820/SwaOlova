namespace SwaOlova.Application.Features.Payments.Commands.RefundPayment;

public sealed record RefundPaymentRequest(
    decimal Amount,
    string Reason);
