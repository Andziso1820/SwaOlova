using FluentValidation;

namespace SwaOlova.Application.Features.Payments.Commands.RefundPayment;

public sealed class RefundPaymentCommandValidator : AbstractValidator<RefundPaymentCommand>
{
    public RefundPaymentCommandValidator()
    {
        RuleFor(x => x.PaymentId).NotEmpty();

        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Amount)
                .GreaterThan(0m);

            RuleFor(x => x.Request.Reason)
                .NotEmpty()
                .MaximumLength(500);
        });
    }
}
