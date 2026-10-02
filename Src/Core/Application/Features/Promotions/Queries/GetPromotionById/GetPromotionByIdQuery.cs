using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Promotions.Dtos;

namespace SwaOlova.Application.Features.Promotions.Queries.GetPromotionById;

public sealed record GetPromotionByIdQuery(Guid PromotionId)
    : QueryBase<PromotionDto>;
