using SwaOlova.Domain.Rider;
using SwaOlova.Domain.Vehicle;

namespace SwaOlova.Application.Features.Riders.Dtos;

internal static class RiderDtoMapper
{
	public static RiderDto ToDto(
		Rider rider,
		Vehicle? vehicle = null,
		RiderLocation? currentLocation = null)
	{
		return new RiderDto(
			rider.Id,
			rider.RiderNumber,
			rider.FirstName,
			rider.LastName,
			$"{rider.FirstName} {rider.LastName}".Trim(),
			rider.Email,
			rider.PhoneNumber,
			rider.DriversLicenseNumber,
			rider.Status,
			rider.StatusReason,
			rider.CreatedDate,
			vehicle is null ? null : ToVehicleDto(vehicle),
			rider.Documents
				.OrderByDescending(x => x.CreatedDate)
				.Select(ToDocumentDto)
				.ToArray(),
			currentLocation is null ? null : ToLocationDto(currentLocation));
	}

	public static RiderVehicleDto ToVehicleDto(Vehicle vehicle)
	{
		return new RiderVehicleDto(
			vehicle.Id,
			vehicle.RegistrationNumber,
			vehicle.VehicleType,
			vehicle.Make,
			vehicle.Model);
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
			document.DocumentType,
			document.FileUrl,
			document.ContentType,
			document.FileSize,
			document.ExpiryDate,
			document.CreatedDate);
	}

	public static RiderActivityDto ToActivityDto(RiderActivity activity)
	{
		return new RiderActivityDto(
			activity.CreatedDate,
			activity.ActivityType,
			activity.Title,
			activity.Description,
			activity.CreatedBy);
	}
}
