using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Commands.SuspendRider;

public sealed class SuspendRiderCommandValidator : AbstractValidator<SuspendRiderCommand>
{
    public SuspendRiderCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
    }
}
