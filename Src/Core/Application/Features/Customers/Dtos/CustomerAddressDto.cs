namespace SwaOlova.Application.Features.Customers.Dtos;

public sealed record CustomerAddressDto(
    Guid Id,
    Guid CustomerId,
    string AddressLine1,
    string Village,
    string Landmark,
    decimal Latitude,
    decimal Longitude,
    string? GatePhotoUrl,
    bool IsDefault);