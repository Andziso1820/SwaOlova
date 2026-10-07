namespace SwaOlova.Application.Features.Locations.Commands.ConfigureCoverage;

public sealed record ConfigureCoverageRequest(
    Guid MerchantId,
    Guid ZoneId,
    Guid? VillageId,
    decimal DeliveryFee,
    int DeliveryTimeMinutes,
    bool IsAvailable);
