using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Dtos;

public sealed record RiderDocumentDto(
	Guid Id,
	Guid RiderId,
	string FileName,
	RiderDocumentType DocumentType,
	string FileUrl,
	string? ContentType,
	long? FileSize,
	DateTime? ExpiryDate,
	DateTime CreatedDate)
{
	public bool IsExpired => ExpiryDate.HasValue && ExpiryDate.Value.Date < DateTime.UtcNow.Date;
}

public sealed record RiderDocumentDownloadDto(
	Guid Id,
	Guid RiderId,
	string FileName,
	string? StoredFileName,
	string? ContentType,
	byte[]? FileData,
	string FileUrl);

public sealed record RiderLocationDto(
	Guid Id,
	Guid RiderId,
	decimal Latitude,
	decimal Longitude,
	DateTime RecordedAt);

public sealed record RiderVehicleDto(
	Guid Id,
	string RegistrationNumber,
	string VehicleType,
	string Make,
	string Model);

public sealed record RiderActivityDto(
	DateTime Date,
	RiderActivityType ActivityType,
	string Title,
	string Description,
	string CreatedBy);

public sealed record RiderDto(
	Guid Id,
	string RiderNumber,
	string FirstName,
	string LastName,
	string FullName,
	string? Email,
	string PhoneNumber,
	string DriversLicenseNumber,
	RiderStatus Status,
	string? StatusReason,
	DateTime CreatedDate,
	RiderVehicleDto? Vehicle,
	IReadOnlyCollection<RiderDocumentDto> Documents,
	RiderLocationDto? CurrentLocation);
