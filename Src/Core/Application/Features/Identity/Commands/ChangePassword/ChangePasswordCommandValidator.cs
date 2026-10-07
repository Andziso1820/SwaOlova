using FluentValidation;
using MediatR;

namespace SwaOlova.Application.Features.Identity.Commands.ChangePassword;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.UserId).NotEmpty();

            RuleFor(x => x.Request.CurrentPassword)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.Request.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .NotEqual(x => x.Request.CurrentPassword)
                .WithMessage("New password must be different from current password");
        });
    }
}
