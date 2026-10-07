using FluentValidation;

namespace SwaOlova.Application.Features.Customers.Commands.RegisterCustomer;

public sealed class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Request.AlternativePhoneNumber)
                .MaximumLength(20);

            RuleFor(x => x.Request.EmailAddress)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Request.EmailAddress));
        });
    }
}
