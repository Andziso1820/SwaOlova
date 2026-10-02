using FluentValidation;

namespace SwaOlova.Application.Features.Customers.Commands.UpdateCustomerAddress;

public sealed class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
{
    public UpdateCustomerAddressCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.AddressId).NotEmpty();
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