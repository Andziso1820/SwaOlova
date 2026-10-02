using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Promotions.Commands.DisablePromotion;

public sealed class DisablePromotionCommandHandler(
    IPromotionRepository promotionRepository)
    : IRequestHandler<DisablePromotionCommand, Result>
{
    public async Task<Result> Handle(DisablePromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await promotionRepository.GetByIdAsync(request.PromotionId, cancellationToken);

        if (promotion is null)
        {
            return Result.Failure($"Promotion with ID '{request.PromotionId}' was not found.");
        }

        promotion.IsActive = false;
        await promotionRepository.UpdateAsync(promotion, cancellationToken);

        return Result.Success();
    }
}
