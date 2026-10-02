using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Queries.GetAvailableRiders;

public sealed class GetAvailableRidersQueryValidator : AbstractValidator<GetAvailableRidersQuery>
{
    public GetAvailableRidersQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
