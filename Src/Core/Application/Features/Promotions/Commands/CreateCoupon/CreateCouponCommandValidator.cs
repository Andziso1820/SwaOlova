using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Commands.CreateCoupon;

public sealed class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Code)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[A-Z0-9_-]+$")
                .WithMessage("Code must contain only uppercase letters, numbers, hyphens, and underscores");

            RuleFor(x => x.Request.DiscountAmount)
                .GreaterThan(0m);

            RuleFor(x => x.Request.ExpiryDate)
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Expiry date must be in the future");
        });
    }
}
