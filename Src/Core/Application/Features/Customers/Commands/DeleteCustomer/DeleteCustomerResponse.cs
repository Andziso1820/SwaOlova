namespace SwaOlova.Application.Features.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerResponse(Guid CustomerId, bool Deleted);