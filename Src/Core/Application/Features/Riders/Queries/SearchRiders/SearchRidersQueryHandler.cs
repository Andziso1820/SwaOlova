using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Queries.SearchRiders;

public sealed record SearchRidersRequest(
    string? Keyword,
    RiderStatus? Status,
    int PageNumber = 1,
    int PageSize = 25);

public sealed record SearchRiderResult(
    Guid Id,
    string RiderNumber,
    string FullName,
    string PhoneNumber,
    RiderStatus Status,
    string DriversLicense,
    DateTime CreatedDate);

public sealed record SearchRidersResponse(
    IReadOnlyCollection<SearchRiderResult> Results,
    IReadOnlyDictionary<RiderStatus, int> StatusCounts,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);

public sealed class SearchRidersQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<SearchRidersQuery, Result<SearchRidersResponse>>
{
    public async Task<Result<SearchRidersResponse>> Handle(SearchRidersQuery request, CancellationToken cancellationToken)
    {
        var input = request.Request;
        var pageNumber = input.PageNumber <= 0 ? 1 : input.PageNumber;
        var pageSize = input.PageSize <= 0 ? 25 : input.PageSize;

        var (items, totalCount) = await riderRepository.SearchAsync(
            input.Keyword, input.Status, pageNumber, pageSize, cancellationToken);

        var statusCounts = await riderRepository.GetStatusCountsAsync(input.Keyword, cancellationToken);

        var results = items
            .Select(r => new SearchRiderResult(
                r.Id,
                r.RiderNumber,
                $"{r.FirstName} {r.LastName}".Trim(),
                r.PhoneNumber,
                r.Status,
                r.DriversLicenseNumber,
                r.CreatedDate))
            .ToArray();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return Result<SearchRidersResponse>.Success(
            new SearchRidersResponse(results, statusCounts, pageNumber, pageSize, totalCount, totalPages));
    }
}
