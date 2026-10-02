namespace SwaOlova.Application.Features.Locations.Dtos;

public sealed record VillageDto(
    Guid Id,
    string Name,
    string Code,
    Guid ZoneId,
    decimal Latitude,
    decimal Longitude,
    bool IsActive);

public sealed record ZoneDto(
    Guid Id,
    string Name,
    string Code,
    IReadOnlyCollection<VillageDto> Villages,
    bool IsActive);

public sealed record CoverageDto(
    Guid Id,
    Guid MerchantId,
    Guid ZoneId,
    Guid? VillageId,
    decimal DeliveryFee,
    int DeliveryTimeMinutes,
    bool IsAvailable);
