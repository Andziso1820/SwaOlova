using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Riders.Queries.SearchRiders;

public sealed class SearchRidersQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<SearchRidersQuery, Result<SearchRidersResponse>>
{
    public async Task<Result<SearchRidersResponse>> Handle(SearchRidersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.Request.PageNumber <= 0 ? 1 : request.Request.PageNumber;
        var pageSize = request.Request.PageSize <= 0 ? 10 : request.Request.PageSize;

        var allRiders = await riderRepository.GetAllAsync(cancellationToken);

        var searchResults = allRiders
            .Where(r =>
            {
                // Apply status filter
                if (!string.IsNullOrWhiteSpace(request.Request.Status) &&
                    !r.Status.ToString().Equals(request.Request.Status, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                // Apply keyword filter (searches name, phone, license)
                if (!string.IsNullOrWhiteSpace(request.Request.Keyword))
                {
                    var keyword = request.Request.Keyword.ToLowerInvariant();
                    var fullName = $"{r.FirstName} {r.LastName}".ToLowerInvariant();
                    if (!fullName.Contains(keyword) &&
                        !r.PhoneNumber.Contains(keyword) &&
                        !r.DriversLicenseNumber.Contains(keyword))
                    {
                        return false;
                    }
                }

                return true;
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new SearchRiderResult(
                r.Id,
                r.RiderNumber,
                $"{r.FirstName} {r.LastName}".Trim(),
                r.PhoneNumber,
                r.Status.ToString(),
                r.DriversLicenseNumber))
            .ToArray();

        var totalCount = allRiders.Count();
        var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling(searchResults.Length / (double)pageSize);

        var response = new SearchRidersResponse(
            searchResults,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);

        return Result<SearchRidersResponse>.Success(response);
    }
}
