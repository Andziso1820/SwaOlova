using FluentValidation;
using MediatR;

namespace SwaOlova.Application.Features.Identity.Commands.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Request.ResetToken)
                .NotEmpty();

            RuleFor(x => x.Request.NewPassword)
                .NotEmpty()
                .MinimumLength(8);
        });
    }
}
