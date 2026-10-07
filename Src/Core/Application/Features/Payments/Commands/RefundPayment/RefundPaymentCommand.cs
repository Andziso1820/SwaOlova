using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Payments.Commands.RefundPayment;

public sealed record RefundPaymentCommand(Guid PaymentId, RefundPaymentRequest Request)
    : CommandBase<Unit>;
