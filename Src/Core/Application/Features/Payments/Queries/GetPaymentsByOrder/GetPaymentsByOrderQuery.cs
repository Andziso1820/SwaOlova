using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentsByOrder;

public sealed record GetPaymentsByOrderQuery(Guid OrderId)
    : QueryBase<GetPaymentsByOrderResponse>;
