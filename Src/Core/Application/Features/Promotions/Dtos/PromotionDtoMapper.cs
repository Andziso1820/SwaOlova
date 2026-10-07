using SwaOlova.Domain.Promotions;

namespace SwaOlova.Application.Features.Promotions.Dtos;

public static class PromotionDtoMapper
{
    public static PromotionDto ToDto(Promotion promotion)
    {
        return new PromotionDto(
            promotion.Id,
            promotion.Name,
            promotion.DiscountValue,
            promotion.StartDate,
            promotion.EndDate,
            promotion.IsActive);
    }

    public static CouponDto ToDto(Coupon coupon)
    {
        return new CouponDto(
            coupon.Id,
            coupon.Code,
            coupon.DiscountAmount,
            coupon.ExpiryDate,
            false); // IsUsed would need tracking in domain if required
    }
}
