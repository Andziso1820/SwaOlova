using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Commands.CreateZone;

public sealed class CreateZoneCommandValidator : AbstractValidator<CreateZoneCommand>
{
    public CreateZoneCommandValidator()
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
        });
    }
}
