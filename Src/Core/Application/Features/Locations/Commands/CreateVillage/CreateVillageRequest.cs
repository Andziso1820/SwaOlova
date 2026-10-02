namespace SwaOlova.Application.Features.Locations.Commands.CreateVillage;

public sealed record CreateVillageRequest(
    string Name,
    string Code,
    Guid ZoneId,
    decimal Latitude,
    decimal Longitude);
