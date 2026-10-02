using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Commands.SetAvailability;

public sealed class SetAvailabilityCommandValidator : AbstractValidator<SetAvailabilityCommand>
{
    public SetAvailabilityCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();
    }
}
