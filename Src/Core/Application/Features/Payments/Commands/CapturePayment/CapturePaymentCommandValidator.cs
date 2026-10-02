using FluentValidation;

namespace SwaOlova.Application.Features.Payments.Commands.CapturePayment;

public sealed class CapturePaymentCommandValidator : AbstractValidator<CapturePaymentCommand>
{
    public CapturePaymentCommandValidator()
    {
        RuleFor(x => x.PaymentId).NotEmpty();

        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.ExternalReference)
                .NotEmpty()
                .MaximumLength(500);
        });
    }
}
