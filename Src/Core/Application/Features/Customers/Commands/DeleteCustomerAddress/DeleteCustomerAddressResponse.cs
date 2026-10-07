namespace SwaOlova.Application.Features.Customers.Commands.DeleteCustomerAddress;

public sealed record DeleteCustomerAddressResponse(Guid CustomerId, Guid AddressId, bool Deleted);