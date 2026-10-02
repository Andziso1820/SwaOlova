using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Promotions.Commands.CreateCoupon;

public sealed record CreateCouponCommand(CreateCouponRequest Request)
    : CommandBase<CreateCouponResponse>;
