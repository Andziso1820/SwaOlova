using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Queries.GetVillageById;

public sealed class GetVillageByIdQueryValidator : AbstractValidator<GetVillageByIdQuery>
{
    public GetVillageByIdQueryValidator()
    {
        RuleFor(x => x.VillageId).NotEmpty();
    }
}
