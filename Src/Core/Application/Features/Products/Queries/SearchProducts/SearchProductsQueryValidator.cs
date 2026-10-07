using FluentValidation;

namespace SwaOlova.Application.Features.Products.Queries.SearchProducts;

public sealed class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
{
    public SearchProductsQueryValidator()
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
