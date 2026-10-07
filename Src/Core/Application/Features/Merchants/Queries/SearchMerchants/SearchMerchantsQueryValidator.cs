using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Queries.SearchMerchants;

public sealed class SearchMerchantsQueryValidator : AbstractValidator<SearchMerchantsQuery>
{
    public SearchMerchantsQueryValidator()
    {
        RuleFor(x => x.SearchTerm)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
