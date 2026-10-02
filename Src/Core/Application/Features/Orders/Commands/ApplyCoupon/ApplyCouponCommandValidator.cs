using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Commands.ApplyCoupon;

public sealed class ApplyCouponCommandValidator : AbstractValidator<ApplyCouponCommand>
{
    public ApplyCouponCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.PromotionId).NotEmpty();
        });
    }
}
