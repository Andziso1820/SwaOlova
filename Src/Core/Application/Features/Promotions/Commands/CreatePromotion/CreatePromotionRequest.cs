namespace SwaOlova.Application.Features.Promotions.Commands.CreatePromotion;

public sealed record CreatePromotionRequest(
    string Name,
    decimal DiscountValue,
    DateTime StartDate,
    DateTime EndDate);
