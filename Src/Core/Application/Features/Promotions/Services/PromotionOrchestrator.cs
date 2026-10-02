using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Commands.ApplyCoupon;
using SwaOlova.Application.Features.Promotions.Commands.ActivatePromotion;
using SwaOlova.Application.Features.Promotions.Commands.CreateCoupon;
using SwaOlova.Application.Features.Promotions.Commands.CreatePromotion;
using SwaOlova.Application.Features.Promotions.Commands.DisablePromotion;
using SwaOlova.Application.Features.Promotions.Dtos;
using SwaOlova.Application.Features.Promotions.Queries.GetActivePromotions;
using SwaOlova.Application.Features.Promotions.Queries.GetCouponByCode;
using SwaOlova.Application.Features.Promotions.Queries.GetCoupons;
using SwaOlova.Application.Features.Promotions.Queries.GetPromotionById;

namespace SwaOlova.Application.Features.Promotions.Services;

public sealed class PromotionOrchestrator(IMediator mediator)
    : IPromotionOrchestrator
{
    public async Task<Result<PromotionDto>> CreatePromotionAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreatePromotionCommand(request);
        var result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Result<PromotionDto>.Success(result.Value.Promotion) : Result<PromotionDto>.Failure(result.Error);
    }

    public async Task<Result> ActivatePromotionAsync(Guid promotionId, CancellationToken cancellationToken = default)
    {
        var command = new ActivatePromotionCommand(promotionId);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result> DisablePromotionAsync(Guid promotionId, CancellationToken cancellationToken = default)
    {
        var command = new DisablePromotionCommand(promotionId);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<CouponDto>> CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreateCouponCommand(request);
        var result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Result<CouponDto>.Success(result.Value.Coupon) : Result<CouponDto>.Failure(result.Error);
    }

    public async Task<Result<ApplyCouponResponse>> ApplyCouponAsync(ApplyCouponRequest request, CancellationToken cancellationToken = default)
    {
        var command = new ApplyCouponCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<PromotionDto>> GetPromotionByIdAsync(Guid promotionId, CancellationToken cancellationToken = default)
    {
        var query = new GetPromotionByIdQuery(promotionId);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<GetCouponsResponse>> GetCouponsAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetCouponsQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<GetActivePromotionsResponse>> GetActivePromotionsAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetActivePromotionsQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<CouponDto>> GetCouponByCodeAsync(string couponCode, CancellationToken cancellationToken = default)
    {
        var query = new GetCouponByCodeQuery(couponCode);
        return await mediator.Send(query, cancellationToken);
    }
}
