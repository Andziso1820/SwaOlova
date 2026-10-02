using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Queries.SearchOrders;

public sealed class SearchOrdersQueryValidator : AbstractValidator<SearchOrdersQuery>
{
    public SearchOrdersQueryValidator()
    {
        RuleFor(x => x.Request).NotNull();
        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Request.PageSize).GreaterThan(0);
        });
    }
}
