using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Commands.AddCustomerAddress;

public sealed record AddCustomerAddressCommand(Guid CustomerId, AddCustomerAddressRequest Request)
    : CommandBase<AddCustomerAddressResponse>;