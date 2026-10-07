using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantsPaged;

public sealed class GetMerchantsPagedQueryValidator : AbstractValidator<GetMerchantsPagedQuery>
{
    public GetMerchantsPagedQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
