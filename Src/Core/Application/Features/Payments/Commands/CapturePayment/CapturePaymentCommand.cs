using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Payments.Commands.CapturePayment;

public sealed record CapturePaymentCommand(Guid PaymentId, CapturePaymentRequest Request)
    : CommandBase<Unit>;
