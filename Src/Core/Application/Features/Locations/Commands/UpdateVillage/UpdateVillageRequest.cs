namespace SwaOlova.Application.Features.Locations.Commands.UpdateVillage;

public sealed record UpdateVillageRequest(
    string Name,
    decimal Latitude,
    decimal Longitude,
    bool IsActive);
