namespace SwaOlova.Application.Features.Payments.Commands.CapturePayment;

public sealed record CapturePaymentRequest(
    string ExternalReference);
