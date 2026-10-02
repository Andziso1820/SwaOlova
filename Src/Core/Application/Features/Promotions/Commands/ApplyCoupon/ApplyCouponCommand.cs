using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Promotions.Commands.ApplyCoupon;

public sealed record ApplyCouponCommand(ApplyCouponRequest Request)
    : CommandBase<ApplyCouponResponse>;
