using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Commands.ApproveRider;

public sealed class ApproveRiderCommandValidator : AbstractValidator<ApproveRiderCommand>
{
    public ApproveRiderCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
    }
}
