using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Commands.UpdateVillage;

public sealed class UpdateVillageCommandValidator : AbstractValidator<UpdateVillageCommand>
{
    public UpdateVillageCommandValidator()
    {
        RuleFor(x => x.VillageId).NotEmpty();

        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.Latitude)
                .InclusiveBetween(-90m, 90m);

            RuleFor(x => x.Request.Longitude)
                .InclusiveBetween(-180m, 180m);
        });
    }
}
