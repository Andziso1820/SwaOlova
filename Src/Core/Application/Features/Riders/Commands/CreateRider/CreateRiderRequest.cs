namespace SwaOlova.Application.Features.Riders.Commands.CreateRider;

public sealed record CreateRiderRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string DriversLicenseNumber);
