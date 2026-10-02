using FluentValidation;

namespace SwaOlova.Application.Features.Customers.Commands.DeleteCustomerAddress;

public sealed class DeleteCustomerAddressCommandValidator : AbstractValidator<DeleteCustomerAddressCommand>
{
    public DeleteCustomerAddressCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.AddressId).NotEmpty();
    }
}