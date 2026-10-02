using FluentValidation;

namespace SwaOlova.Application.Features.Identity.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Request.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]")
                .WithMessage("Password must contain at least one digit");

            RuleFor(x => x.Request.FullName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Request.Role)
                .NotEmpty()
                .Must(r => new[] { "Admin", "Merchant", "Rider", "Customer", "Dispatcher" }.Contains(r))
                .WithMessage("Role must be one of: Admin, Merchant, Rider, Customer, Dispatcher");
        });
    }
}
