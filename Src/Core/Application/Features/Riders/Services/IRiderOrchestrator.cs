using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Services;

/// <summary>
/// Orchestrates complex rider operations involving multiple repositories and domain logic.
/// Follows domain entity pattern as per user preferences for passing entities through layers.
/// </summary>
public interface IRiderOrchestrator
{
    /// <summary>
    /// Creates a new rider in pending approval state.
    /// </summary>
    /// <param name="firstName">Rider first name</param>
    /// <param name="lastName">Rider last name</param>
    /// <param name="phoneNumber">Rider phone number</param>
    /// <param name="driversLicenseNumber">Driver's license number</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with created rider DTO</returns>
    Task<Result<RiderDto>> CreateRiderAsync(
        string firstName,
        string lastName,
        string phoneNumber,
        string driversLicenseNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Approves a pending rider and sets status to available.
    /// </summary>
    /// <param name="riderId">Rider ID to approve</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with approved rider DTO</returns>
    Task<Result<RiderDto>> ApproveRiderAsync(Guid riderId, CancellationToken cancellationToken);

    /// <summary>
    /// Suspends a rider, preventing them from accepting deliveries.
    /// </summary>
    /// <param name="riderId">Rider ID to suspend</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with suspended rider DTO</returns>
    Task<Result<RiderDto>> SuspendRiderAsync(Guid riderId, CancellationToken cancellationToken);

    /// <summary>
    /// Sets rider availability status (available/offline).
    /// </summary>
    /// <param name="riderId">Rider ID</param>
    /// <param name="isAvailable">Whether rider is available for deliveries</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating success or failure</returns>
    Task<Result> SetAvailabilityAsync(Guid riderId, bool isAvailable, CancellationToken cancellationToken);

    /// <summary>
    /// Updates rider's current location with GPS coordinates.
    /// </summary>
    /// <param name="riderId">Rider ID</param>
    /// <param name="latitude">Latitude coordinate</param>
    /// <param name="longitude">Longitude coordinate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with location DTO</returns>
    Task<Result<RiderLocationDto>> UpdateLocationAsync(
        Guid riderId,
        decimal latitude,
        decimal longitude,
        CancellationToken cancellationToken);

    /// <summary>
    /// Uploads a rider document (license, insurance, etc.).
    /// </summary>
    /// <param name="riderId">Rider ID</param>
    /// <param name="fileName">Document file name</param>
    /// <param name="fileUrl">Document file URL/path</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with document DTO</returns>
    Task<Result<RiderDocumentDto>> UploadDocumentAsync(
        Guid riderId,
        string fileName,
        string fileUrl,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets all available riders for assignment.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result with list of available riders</returns>
    Task<Result<IReadOnlyCollection<RiderDto>>> GetAvailableRidersAsync(CancellationToken cancellationToken);
}
