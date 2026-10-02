using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Payments.Commands.CancelPayment;

public sealed record CancelPaymentCommand(Guid PaymentId)
    : CommandBase<Unit>;
