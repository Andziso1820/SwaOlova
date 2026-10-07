namespace SwaOlova.Application.Features.Promotions.Dtos;

public sealed record PromotionDto(
    Guid Id,
    string Name,
    decimal DiscountValue,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive);

public sealed record CouponDto(
    Guid Id,
    string Code,
    decimal DiscountAmount,
    DateTime ExpiryDate,
    bool IsUsed);
