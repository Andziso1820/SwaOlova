using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Dtos;

internal static class RiderDtoMapper
{
    public static RiderDto ToDto(
        Rider rider,
        IReadOnlyCollection<RiderDocumentDto>? documents = null,
        RiderLocationDto? currentLocation = null)
    {
        return new RiderDto(
            rider.Id,
            rider.RiderNumber,
            rider.FirstName,
            rider.LastName,
            $"{rider.FirstName} {rider.LastName}".Trim(),
            string.Empty,
            rider.PhoneNumber,
            rider.DriversLicenseNumber,
            rider.Status,
            null,
            null,
            null,
            null,
            null,
            documents ?? Array.Empty<RiderDocumentDto>(),
            currentLocation);
    }

    public static RiderLocationDto ToLocationDto(RiderLocation location)
    {
        return new RiderLocationDto(
            location.Id,
            location.RiderId,
            location.Latitude,
            location.Longitude,
            location.RecordedAt);
    }

    public static RiderDocumentDto ToDocumentDto(RiderDocument document)
    {
        return new RiderDocumentDto(
            document.Id,
            document.RiderId,
            document.FileName,
            document.FileUrl);
    }
}
