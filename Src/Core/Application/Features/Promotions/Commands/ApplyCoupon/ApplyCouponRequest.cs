namespace SwaOlova.Application.Features.Promotions.Commands.ApplyCoupon;

public sealed record ApplyCouponRequest(
    Guid OrderId,
    string CouponCode);

public sealed record ApplyCouponResponse(
    Guid OrderId,
    string CouponCode,
    decimal DiscountAmount,
    decimal FinalAmount);
