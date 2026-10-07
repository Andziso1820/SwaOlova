using FluentValidation;

namespace SwaOlova.Application.Features.Notifications.Commands.SendEmail;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.EmailAddress)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Request.Subject)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Request.Message)
                .NotEmpty()
                .MaximumLength(10000);
        });
    }
}
