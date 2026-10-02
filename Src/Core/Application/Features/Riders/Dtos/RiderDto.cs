using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Dtos;

public sealed record RiderDocumentDto(
    Guid Id,
    Guid RiderId,
    string FileName,
    string FileUrl);

public sealed record RiderLocationDto(
    Guid Id,
    Guid RiderId,
    decimal Latitude,
    decimal Longitude,
    DateTime RecordedAt);

public sealed record RiderDto(
    Guid Id,
    string RiderNumber,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    string DriversLicenseNumber,
    RiderStatus Status,
    decimal? Rating,
    string? VehicleType,
    string? LicensePlate,
    string? VehicleModel,
    DateTime? InsuranceExpiryDate,
    IReadOnlyCollection<RiderDocumentDto> Documents,
    RiderLocationDto? CurrentLocation);
