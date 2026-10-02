using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Dtos;

namespace SwaOlova.Application.Features.Promotions.Queries.GetCoupons;

public sealed class GetCouponsQueryHandler(
    IPromotionRepository promotionRepository)
    : IRequestHandler<GetCouponsQuery, Result<GetCouponsResponse>>
{
    public async Task<Result<GetCouponsResponse>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
    {
        // GetAllAsync would need to be added to IPromotionRepository
        // For now, using an empty collection placeholder
        var coupons = Array.Empty<CouponDto>();

        var response = new GetCouponsResponse(coupons);
        return await Task.FromResult(Result<GetCouponsResponse>.Success(response));
    }
}
