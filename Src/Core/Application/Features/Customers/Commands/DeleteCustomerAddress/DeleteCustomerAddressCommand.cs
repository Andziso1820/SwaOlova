using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Commands.DeleteCustomerAddress;

public sealed record DeleteCustomerAddressCommand(Guid CustomerId, Guid AddressId)
    : CommandBase<DeleteCustomerAddressResponse>;