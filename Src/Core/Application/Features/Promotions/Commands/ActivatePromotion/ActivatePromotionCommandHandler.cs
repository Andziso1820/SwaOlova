using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Promotions.Commands.ActivatePromotion;

public sealed class ActivatePromotionCommandHandler(
    IPromotionRepository promotionRepository)
    : IRequestHandler<ActivatePromotionCommand, Result>
{
    public async Task<Result> Handle(ActivatePromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await promotionRepository.GetByIdAsync(request.PromotionId, cancellationToken);

        if (promotion is null)
        {
            return Result.Failure($"Promotion with ID '{request.PromotionId}' was not found.");
        }

        promotion.IsActive = true;
        await promotionRepository.UpdateAsync(promotion, cancellationToken);

        return Result.Success();
    }
}
