using FluentValidation;

namespace SwaOlova.Application.Features.Payments.Commands.ProcessWebhook;

public sealed class ProcessWebhookCommandValidator : AbstractValidator<ProcessWebhookCommand>
{
    public ProcessWebhookCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.ExternalReference)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Request.Status)
                .NotEmpty()
                .MaximumLength(50)
                .Must(s => s is "success" or "failed" or "cancelled")
                .WithMessage("Status must be 'success', 'failed', or 'cancelled'");
        });
    }
}
