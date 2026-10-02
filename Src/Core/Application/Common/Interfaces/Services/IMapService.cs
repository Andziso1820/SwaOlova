namespace SwaOlova.Application.Common.Interfaces.Services;

public interface IMapService
{
    Task<string?> ReverseGeocodeAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken = default);
}
