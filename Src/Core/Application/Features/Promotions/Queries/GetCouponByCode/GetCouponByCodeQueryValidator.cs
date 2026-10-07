using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Queries.GetCouponByCode;

public sealed class GetCouponByCodeQueryValidator : AbstractValidator<GetCouponByCodeQuery>
{
    public GetCouponByCodeQueryValidator()
    {
        RuleFor(x => x.CouponCode)
            .NotEmpty()
            .MaximumLength(50);
    }
}
