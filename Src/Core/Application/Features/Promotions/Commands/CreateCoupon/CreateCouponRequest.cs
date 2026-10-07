namespace SwaOlova.Application.Features.Promotions.Commands.CreateCoupon;

public sealed record CreateCouponRequest(
    string Code,
    decimal DiscountAmount,
    DateTime ExpiryDate);
