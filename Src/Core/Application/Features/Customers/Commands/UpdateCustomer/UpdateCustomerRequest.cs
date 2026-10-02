namespace SwaOlova.Application.Features.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerRequest(
    string FirstName,
    string LastName,
    string? AlternativePhoneNumber,
    string? EmailAddress);