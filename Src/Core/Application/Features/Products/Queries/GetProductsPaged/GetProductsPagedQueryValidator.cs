using FluentValidation;

namespace SwaOlova.Application.Features.Products.Queries.GetProductsPaged;

public sealed class GetProductsPagedQueryValidator : AbstractValidator<GetProductsPagedQuery>
{
    public GetProductsPagedQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
