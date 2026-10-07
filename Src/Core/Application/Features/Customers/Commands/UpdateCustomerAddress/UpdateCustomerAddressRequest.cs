namespace SwaOlova.Application.Features.Customers.Commands.UpdateCustomerAddress;

public sealed record UpdateCustomerAddressRequest(
    string AddressLine1,
    string Village,
    string Landmark,
    decimal Latitude,
    decimal Longitude,
    string? GatePhotoUrl,
    bool IsDefault);