using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Payments.Commands.CreatePayment;
using SwaOlova.Application.Features.Payments.Dtos;
using SwaOlova.Application.Features.Payments.Queries.GetPaymentsByOrder;
using SwaOlova.Application.Features.Payments.Queries.GetPaymentRefunds;

namespace SwaOlova.Application.Features.Payments.Services;

public interface IPaymentOrchestrator
{
    Task<Result<PaymentDto>> CreatePaymentAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default);

    Task<Result> CapturePaymentAsync(
        Guid paymentId,
        string externalReference,
        CancellationToken cancellationToken = default);

    Task<Result> RefundPaymentAsync(
        Guid paymentId,
        decimal amount,
        string reason,
        CancellationToken cancellationToken = default);

    Task<Result> CancelPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);

    Task<Result> ProcessWebhookAsync(
        string externalReference,
        string status,
        string? failureReason,
        CancellationToken cancellationToken = default);

    Task<Result<PaymentDto>> GetPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);

    Task<Result<GetPaymentsByOrderResponse>> GetPaymentsByOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default);

    Task<Result<GetPaymentRefundsResponse>> GetPaymentRefundsAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);
}
