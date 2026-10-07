using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Commands.DisablePromotion;

public sealed class DisablePromotionCommandValidator : AbstractValidator<DisablePromotionCommand>
{
    public DisablePromotionCommandValidator()
    {
        RuleFor(x => x.PromotionId).NotEmpty();
    }
}
