using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Dtos;
using SwaOlova.Domain.Promotions;

namespace SwaOlova.Application.Features.Promotions.Commands.CreateCoupon;

public sealed class CreateCouponCommandHandler(
    ICouponRepository couponRepository)
    : IRequestHandler<CreateCouponCommand, Result<CreateCouponResponse>>
{
    public async Task<Result<CreateCouponResponse>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = request.Request.Code.Trim().ToUpper(),
            DiscountAmount = request.Request.DiscountAmount,
            ExpiryDate = request.Request.ExpiryDate
        };

        await couponRepository.AddAsync(coupon, cancellationToken);

        var response = new CreateCouponResponse(PromotionDtoMapper.ToDto(coupon));
        return Result<CreateCouponResponse>.Success(response);
    }
}
