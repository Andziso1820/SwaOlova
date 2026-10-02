using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.ApplyCoupon;

public sealed record ApplyCouponCommand(Guid OrderId, ApplyCouponRequest Request)
    : CommandBase<ApplyCouponResponse>;
