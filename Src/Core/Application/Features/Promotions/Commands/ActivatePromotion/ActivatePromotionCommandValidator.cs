using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Commands.ActivatePromotion;

public sealed class ActivatePromotionCommandValidator : AbstractValidator<ActivatePromotionCommand>
{
    public ActivatePromotionCommandValidator()
    {
        RuleFor(x => x.PromotionId).NotEmpty();
    }
}
