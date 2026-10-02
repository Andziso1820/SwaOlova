using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Commands.ApplyCoupon;
using SwaOlova.Application.Features.Promotions.Commands.CreateCoupon;
using SwaOlova.Application.Features.Promotions.Commands.CreatePromotion;
using SwaOlova.Application.Features.Promotions.Dtos;
using SwaOlova.Application.Features.Promotions.Queries.GetActivePromotions;
using SwaOlova.Application.Features.Promotions.Queries.GetCouponByCode;
using SwaOlova.Application.Features.Promotions.Queries.GetCoupons;

namespace SwaOlova.Application.Features.Promotions.Services;

public interface IPromotionOrchestrator
{
    Task<Result<PromotionDto>> CreatePromotionAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default);
    Task<Result> ActivatePromotionAsync(Guid promotionId, CancellationToken cancellationToken = default);
    Task<Result> DisablePromotionAsync(Guid promotionId, CancellationToken cancellationToken = default);
    Task<Result<CouponDto>> CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default);
    Task<Result<ApplyCouponResponse>> ApplyCouponAsync(ApplyCouponRequest request, CancellationToken cancellationToken = default);
    Task<Result<PromotionDto>> GetPromotionByIdAsync(Guid promotionId, CancellationToken cancellationToken = default);
    Task<Result<GetCouponsResponse>> GetCouponsAsync(CancellationToken cancellationToken = default);
    Task<Result<GetActivePromotionsResponse>> GetActivePromotionsAsync(CancellationToken cancellationToken = default);
    Task<Result<CouponDto>> GetCouponByCodeAsync(string couponCode, CancellationToken cancellationToken = default);
}
