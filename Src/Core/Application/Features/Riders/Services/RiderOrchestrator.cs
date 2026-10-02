using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Services;

public sealed class RiderOrchestrator(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRiderOrchestrator
{
    public async Task<Result<RiderDto>> CreateRiderAsync(
        string firstName,
        string lastName,
        string phoneNumber,
        string driversLicenseNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            var rider = new Rider
            {
                Id = Guid.NewGuid(),
                RiderNumber = $"RID-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                PhoneNumber = phoneNumber.Trim(),
                DriversLicenseNumber = driversLicenseNumber.Trim(),
                Status = RiderStatus.PendingApproval
            };

            await riderRepository.AddAsync(rider, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RiderDto>.Success(RiderDtoMapper.ToDto(rider));
        }
        catch (Exception ex)
        {
            return Result<RiderDto>.Failure($"Error creating rider: {ex.Message}");
        }
    }

    public async Task<Result<RiderDto>> ApproveRiderAsync(Guid riderId, CancellationToken cancellationToken)
    {
        try
        {
            var rider = await riderRepository.GetByIdAsync(riderId, cancellationToken);
            if (rider is null)
                return Result<RiderDto>.Failure($"Rider with ID '{riderId}' not found.");

            if (rider.Status != RiderStatus.PendingApproval)
                return Result<RiderDto>.Failure("Only riders pending approval can be approved.");

            rider.Status = RiderStatus.Available;
            await riderRepository.UpdateAsync(rider, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RiderDto>.Success(RiderDtoMapper.ToDto(rider));
        }
        catch (Exception ex)
        {
            return Result<RiderDto>.Failure($"Error approving rider: {ex.Message}");
        }
    }

    public async Task<Result<RiderDto>> SuspendRiderAsync(Guid riderId, CancellationToken cancellationToken)
    {
        try
        {
            var rider = await riderRepository.GetByIdAsync(riderId, cancellationToken);
            if (rider is null)
                return Result<RiderDto>.Failure($"Rider with ID '{riderId}' not found.");

            if (rider.Status == RiderStatus.Suspended)
                return Result<RiderDto>.Failure("Rider is already suspended.");

            rider.Status = RiderStatus.Suspended;
            await riderRepository.UpdateAsync(rider, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RiderDto>.Success(RiderDtoMapper.ToDto(rider));
        }
        catch (Exception ex)
        {
            return Result<RiderDto>.Failure($"Error suspending rider: {ex.Message}");
        }
    }

    public async Task<Result> SetAvailabilityAsync(Guid riderId, bool isAvailable, CancellationToken cancellationToken)
    {
        try
        {
            var rider = await riderRepository.GetByIdAsync(riderId, cancellationToken);
            if (rider is null)
                return Result.Failure($"Rider with ID '{riderId}' not found.");

            if (rider.Status == RiderStatus.Suspended)
                return Result.Failure("Suspended riders cannot change availability.");

            var newStatus = isAvailable ? RiderStatus.Available : RiderStatus.Offline;
            if (rider.Status == newStatus)
                return Result.Failure($"Rider is already {newStatus.ToString().ToLower()}.");

            rider.Status = newStatus;
            await riderRepository.UpdateAsync(rider, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error setting availability: {ex.Message}");
        }
    }

    public async Task<Result<RiderLocationDto>> UpdateLocationAsync(
        Guid riderId,
        decimal latitude,
        decimal longitude,
        CancellationToken cancellationToken)
    {
        try
        {
            var rider = await riderRepository.GetByIdAsync(riderId, cancellationToken);
            if (rider is null)
                return Result<RiderLocationDto>.Failure($"Rider with ID '{riderId}' not found.");

            var location = new RiderLocation
            {
                Id = Guid.NewGuid(),
                RiderId = riderId,
                Latitude = latitude,
                Longitude = longitude,
                RecordedAt = DateTime.UtcNow
            };

            // Note: In production, would save location to repository
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RiderLocationDto>.Success(RiderDtoMapper.ToLocationDto(location));
        }
        catch (Exception ex)
        {
            return Result<RiderLocationDto>.Failure($"Error updating location: {ex.Message}");
        }
    }

    public async Task<Result<RiderDocumentDto>> UploadDocumentAsync(
        Guid riderId,
        string fileName,
        string fileUrl,
        CancellationToken cancellationToken)
    {
        try
        {
            var rider = await riderRepository.GetByIdAsync(riderId, cancellationToken);
            if (rider is null)
                return Result<RiderDocumentDto>.Failure($"Rider with ID '{riderId}' not found.");

            var document = new RiderDocument
            {
                Id = Guid.NewGuid(),
                RiderId = riderId,
                FileName = fileName.Trim(),
                FileUrl = fileUrl.Trim()
            };

            // Note: In production, would save document to repository
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RiderDocumentDto>.Success(RiderDtoMapper.ToDocumentDto(document));
        }
        catch (Exception ex)
        {
            return Result<RiderDocumentDto>.Failure($"Error uploading document: {ex.Message}");
        }
    }

    public async Task<Result<IReadOnlyCollection<RiderDto>>> GetAvailableRidersAsync(CancellationToken cancellationToken)
    {
        try
        {
            var allRiders = await riderRepository.GetAllAsync(cancellationToken);
            var availableRiders = allRiders
                .Where(r => r.Status == RiderStatus.Available)
                .Select(r => RiderDtoMapper.ToDto(r))
                .ToArray();

            return Result<IReadOnlyCollection<RiderDto>>.Success(availableRiders);
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyCollection<RiderDto>>.Failure($"Error retrieving available riders: {ex.Message}");
        }
    }
}
