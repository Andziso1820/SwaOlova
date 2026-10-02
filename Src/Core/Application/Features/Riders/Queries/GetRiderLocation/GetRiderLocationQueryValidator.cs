using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderLocation;

public sealed class GetRiderLocationQueryValidator : AbstractValidator<GetRiderLocationQuery>
{
    public GetRiderLocationQueryValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
    }
}
