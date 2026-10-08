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

        var (items, totalCount) = await riderRepository.SearchAsync(
            null, RiderStatus.Available, pageNumber, pageSize, cancellationToken);

        var riders = items
            .Select(r => new RiderSummaryDto(
                r.Id,
                r.RiderNumber,
                $"{r.FirstName} {r.LastName}".Trim(),
                r.PhoneNumber,
                r.Status.ToString()))
            .ToArray();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return Result<GetAvailableRidersResponse>.Success(
            new GetAvailableRidersResponse(riders, pageNumber, pageSize, totalCount, totalPages));
    }
}
