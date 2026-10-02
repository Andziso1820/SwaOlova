using FluentValidation;

namespace SwaOlova.Application.Features.Payments.Commands.CreatePayment;

public sealed class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.OrderId).NotEmpty();

            RuleFor(x => x.Request.Amount)
                .GreaterThan(0m);

            RuleFor(x => x.Request.PaymentMethod)
                .NotEmpty()
                .MaximumLength(50);
        });
    }
}
