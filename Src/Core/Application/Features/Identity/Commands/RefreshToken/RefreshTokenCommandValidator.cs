using FluentValidation;

namespace SwaOlova.Application.Features.Identity.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.AccessToken).NotEmpty();
            RuleFor(x => x.Request.RefreshToken).NotEmpty();
        });
    }
}
