using FluentValidation;

namespace SwaOlova.Application.Features.Customers.Commands.AddCustomerAddress;

public sealed class AddCustomerAddressCommandValidator : AbstractValidator<AddCustomerAddressCommand>
{
    public AddCustomerAddressCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.AddressLine1)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Request.Village)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Request.Landmark)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Request.GatePhotoUrl)
                .MaximumLength(500);
        });
    }
}