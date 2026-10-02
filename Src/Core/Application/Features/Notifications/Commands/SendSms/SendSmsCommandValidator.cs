using FluentValidation;

namespace SwaOlova.Application.Features.Notifications.Commands.SendSms;

public sealed class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
{
    public SendSmsCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be a valid E.164 format");

            RuleFor(x => x.Request.Message)
                .NotEmpty()
                .MaximumLength(160);
        });
    }
}
