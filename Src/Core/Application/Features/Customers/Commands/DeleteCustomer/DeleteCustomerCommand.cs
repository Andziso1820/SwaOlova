using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(Guid CustomerId)
    : CommandBase<DeleteCustomerResponse>;