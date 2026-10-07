using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;

public sealed class GetRiderDeliveriesQueryValidator : AbstractValidator<GetRiderDeliveriesQuery>
{
    public GetRiderDeliveriesQueryValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
