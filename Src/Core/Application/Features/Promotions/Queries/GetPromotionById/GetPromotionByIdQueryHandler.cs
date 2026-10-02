using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Dtos;

namespace SwaOlova.Application.Features.Promotions.Queries.GetPromotionById;

public sealed class GetPromotionByIdQueryHandler(
    IPromotionRepository promotionRepository)
    : IRequestHandler<GetPromotionByIdQuery, Result<PromotionDto>>
{
    public async Task<Result<PromotionDto>> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
    {
        var promotion = await promotionRepository.GetByIdAsync(request.PromotionId, cancellationToken);

        if (promotion is null)
        {
            return Result<PromotionDto>.Failure($"Promotion with ID '{request.PromotionId}' was not found.");
        }

        var dto = PromotionDtoMapper.ToDto(promotion);
        return Result<PromotionDto>.Success(dto);
    }
}
