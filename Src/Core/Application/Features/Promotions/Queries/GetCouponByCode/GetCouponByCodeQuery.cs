using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Promotions.Dtos;

namespace SwaOlova.Application.Features.Promotions.Queries.GetCouponByCode;

public sealed record GetCouponByCodeQuery(string CouponCode)
    : QueryBase<CouponDto>;
