using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Commands.ApplyCoupon;

public sealed class ApplyCouponCommandValidator : AbstractValidator<ApplyCouponCommand>
{
    public ApplyCouponCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.OrderId).NotEmpty();

            RuleFor(x => x.Request.CouponCode)
                .NotEmpty()
                .MaximumLength(50);
        });
    }
}
