using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Queries.GetAvailableRiders;

public sealed class GetAvailableRidersQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<GetAvailableRidersQuery, Result<GetAvailableRidersResponse>>
{
    public async Task<Result<GetAvailableRidersResponse>> Handle(GetAvailableRidersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        // Note: This would require a GetAvailableAsync method on IRiderRepository
        // For now, we retrieve all and filter in memory, but in production this should be at the database level
        var allRiders = await riderRepository.GetAllAsync(cancellationToken);

        var availableRiders = allRiders
            .Where(r => r.Status == RiderStatus.Available)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new RiderSummaryDto(
                r.Id,
                r.RiderNumber,
                $"{r.FirstName} {r.LastName}".Trim(),
                r.PhoneNumber,
                r.Status.ToString()))
            .ToArray();

        var totalCount = allRiders.Count(r => r.Status == RiderStatus.Available);
        var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new GetAvailableRidersResponse(
            availableRiders,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);

        return Result<GetAvailableRidersResponse>.Success(response);
    }
}
