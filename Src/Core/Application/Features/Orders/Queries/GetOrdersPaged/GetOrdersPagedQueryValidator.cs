using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrdersPaged;

public sealed class GetOrdersPagedQueryValidator : AbstractValidator<GetOrdersPagedQuery>
{
    public GetOrdersPagedQueryValidator()
    {
        RuleFor(x => x.Request).NotNull();
        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Request.PageSize).GreaterThan(0);
        });
    }
}
