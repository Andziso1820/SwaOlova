using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid CustomerId, UpdateCustomerRequest Request)
    : CommandBase<UpdateCustomerResponse>;