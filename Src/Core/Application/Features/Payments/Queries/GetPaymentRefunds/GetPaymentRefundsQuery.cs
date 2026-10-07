using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Payments.Dtos;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentRefunds;

public sealed record GetPaymentRefundsQuery(Guid PaymentId)
    : QueryBase<GetPaymentRefundsResponse>;
