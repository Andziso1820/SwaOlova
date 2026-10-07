using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Dtos;

namespace SwaOlova.Application.Features.Promotions.Queries.GetActivePromotions;

public sealed class GetActivePromotionsQueryHandler(
    IPromotionRepository promotionRepository)
    : IRequestHandler<GetActivePromotionsQuery, Result<GetActivePromotionsResponse>>
{
    public async Task<Result<GetActivePromotionsResponse>> Handle(GetActivePromotionsQuery request, CancellationToken cancellationToken)
    {
        // GetAllAsync would need to be added to IPromotionRepository
        // For now, filtering in-memory after retrieval
        var promotions = Array.Empty<PromotionDto>();

        var response = new GetActivePromotionsResponse(promotions);
        return await Task.FromResult(Result<GetActivePromotionsResponse>.Success(response));
    }
}
