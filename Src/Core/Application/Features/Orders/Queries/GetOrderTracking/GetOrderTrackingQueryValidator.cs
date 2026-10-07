using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrderTracking;

public sealed class GetOrderTrackingQueryValidator : AbstractValidator<GetOrderTrackingQuery>
{
    public GetOrderTrackingQueryValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
