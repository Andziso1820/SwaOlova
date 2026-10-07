using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Promotions.Dtos;

namespace SwaOlova.Application.Features.Promotions.Queries.GetActivePromotions;

public sealed record GetActivePromotionsQuery
    : QueryBase<GetActivePromotionsResponse>;

public sealed record GetActivePromotionsResponse(
    IReadOnlyCollection<PromotionDto> Promotions);
