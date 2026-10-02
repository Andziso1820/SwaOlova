using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.ApplyCoupon;

public sealed class ApplyCouponCommandHandler(
    IOrderRepository orderRepository,
    IPromotionRepository promotionRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ApplyCouponCommand, Result<ApplyCouponResponse>>
{
    public async Task<Result<ApplyCouponResponse>> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<ApplyCouponResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status != OrderStatus.Pending)
        {
            return Result<ApplyCouponResponse>.Failure("Coupon can only be applied to pending orders.");
        }

        var promotion = await promotionRepository.GetByIdAsync(request.Request.PromotionId, cancellationToken);
        if (promotion is null)
        {
            return Result<ApplyCouponResponse>.Failure($"Promotion with ID '{request.Request.PromotionId}' was not found.");
        }

        var now = DateTime.UtcNow;
        if (!promotion.IsActive || promotion.StartDate > now || promotion.EndDate < now)
        {
            return Result<ApplyCouponResponse>.Failure("Promotion is not active.");
        }

        var discountValue = Math.Min(promotion.DiscountValue, order.SubTotal);
        order.Total = order.SubTotal - discountValue + order.DeliveryFee;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ApplyCouponResponse>.Success(
            new ApplyCouponResponse(OrderDtoMapper.ToDto(order), promotion.Id, discountValue));
    }
}
