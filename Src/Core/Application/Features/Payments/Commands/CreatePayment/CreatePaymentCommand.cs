using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Payments.Commands.CreatePayment;

public sealed record CreatePaymentCommand(CreatePaymentRequest Request)
    : CommandBase<CreatePaymentResponse>;
