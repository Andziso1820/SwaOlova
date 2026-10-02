using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Commands.CreatePromotion;

public sealed class CreatePromotionCommandValidator : AbstractValidator<CreatePromotionCommand>
{
    public CreatePromotionCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Request.DiscountValue)
                .GreaterThan(0m);

            RuleFor(x => x.Request.StartDate)
                .NotEmpty()
                .LessThan(x => x.Request.EndDate)
                .WithMessage("Start date must be before end date");

            RuleFor(x => x.Request.EndDate)
                .NotEmpty()
                .GreaterThan(x => x.Request.StartDate);
        });
    }
}
