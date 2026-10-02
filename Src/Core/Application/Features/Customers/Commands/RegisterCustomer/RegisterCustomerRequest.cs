namespace SwaOlova.Application.Features.Customers.Commands.RegisterCustomer;

public sealed record RegisterCustomerRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? AlternativePhoneNumber,
    string? EmailAddress);
