using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Commands.CreateRider;

public sealed class CreateRiderCommandValidator : AbstractValidator<CreateRiderCommand>
{
    public CreateRiderCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Request.DriversLicenseNumber)
                .NotEmpty()
                .MaximumLength(50);
        });
    }
}
