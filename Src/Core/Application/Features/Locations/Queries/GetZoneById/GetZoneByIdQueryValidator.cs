using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Queries.GetZoneById;

public sealed class GetZoneByIdQueryValidator : AbstractValidator<GetZoneByIdQuery>
{
    public GetZoneByIdQueryValidator()
    {
        RuleFor(x => x.ZoneId).NotEmpty();
    }
}
