using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrders;

public sealed class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
