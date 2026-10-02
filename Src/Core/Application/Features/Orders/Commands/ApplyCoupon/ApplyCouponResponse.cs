using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Commands.ApplyCoupon;

public sealed record ApplyCouponResponse(OrderDto Order, Guid PromotionId, decimal DiscountValue);
