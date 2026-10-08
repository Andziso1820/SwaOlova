using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;
using SwaOlova.Domain.Vehicle;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IRiderRepository : IRepository<Rider>
{
	Task<(IReadOnlyCollection<Rider> Items, int TotalCount)> SearchAsync(
		string? keyword,
		RiderStatus? status,
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken = default);

	Task<IReadOnlyDictionary<RiderStatus, int>> GetStatusCountsAsync(string? keyword, CancellationToken cancellationToken = default);

	Task<bool> PhoneNumberExistsAsync(string phoneNumber, Guid? excludeRiderId = null, CancellationToken cancellationToken = default);

	Task<bool> DriversLicenseExistsAsync(string driversLicenseNumber, Guid? excludeRiderId = null, CancellationToken cancellationToken = default);

	Task<IReadOnlyCollection<RiderActivity>> GetActivityHistoryAsync(Guid riderId, CancellationToken cancellationToken = default);

	Task AddActivityAsync(RiderActivity activity, CancellationToken cancellationToken = default);

	Task<RiderDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default);

	Task AddDocumentAsync(RiderDocument document, CancellationToken cancellationToken = default);

	Task<RiderLocation?> GetLatestLocationAsync(Guid riderId, CancellationToken cancellationToken = default);

	Task AddLocationAsync(RiderLocation location, CancellationToken cancellationToken = default);

	Task<Vehicle?> GetVehicleAsync(Guid riderId, CancellationToken cancellationToken = default);

	Task<bool> RegistrationNumberExistsAsync(string registrationNumber, Guid? excludeRiderId = null, CancellationToken cancellationToken = default);

	Task AddVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
}
