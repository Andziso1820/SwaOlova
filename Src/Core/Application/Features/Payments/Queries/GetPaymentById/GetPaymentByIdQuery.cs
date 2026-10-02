using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Payments.Dtos;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentById;

public sealed record GetPaymentByIdQuery(Guid PaymentId)
    : QueryBase<PaymentDto>;
