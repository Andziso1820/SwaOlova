using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Commands.RegisterCustomer;

public sealed record RegisterCustomerCommand(RegisterCustomerRequest Request)
    : CommandBase<RegisterCustomerResponse>;
