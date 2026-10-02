using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Commands.UpdateCustomerAddress;

public sealed record UpdateCustomerAddressCommand(Guid CustomerId, Guid AddressId, UpdateCustomerAddressRequest Request)
    : CommandBase<UpdateCustomerAddressResponse>;