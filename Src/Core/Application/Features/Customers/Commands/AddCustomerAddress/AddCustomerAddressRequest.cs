namespace SwaOlova.Application.Features.Customers.Commands.AddCustomerAddress;

public sealed record AddCustomerAddressRequest(
    string AddressLine1,
    string Village,
    string Landmark,
    decimal Latitude,
    decimal Longitude,
    string? GatePhotoUrl,
    bool IsDefault);