using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Commands.CreateVillage;

public sealed class CreateVillageCommandValidator : AbstractValidator<CreateVillageCommand>
{
    public CreateVillageCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.Code)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[A-Z0-9_]+$")
                .WithMessage("Code must contain only uppercase letters, numbers, and underscores");

            RuleFor(x => x.Request.ZoneId).NotEmpty();

            RuleFor(x => x.Request.Latitude)
                .InclusiveBetween(-90m, 90m);

            RuleFor(x => x.Request.Longitude)
                .InclusiveBetween(-180m, 180m);
        });
    }
}
