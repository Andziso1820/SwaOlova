using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Locations.Queries.GetCoverageByArea;

public sealed class GetCoverageByAreaQueryHandler
    : IRequestHandler<GetCoverageByAreaQuery, Result<GetCoverageByAreaResponse>>
{
    public async Task<Result<GetCoverageByAreaResponse>> Handle(GetCoverageByAreaQuery request, CancellationToken cancellationToken)
    {
        // In a real scenario, this would query coverage configurations from a repository
        // For now, returning empty collection
        var coverage = Array.Empty<SwaOlova.Application.Features.Locations.Dtos.CoverageDto>();
        var response = new GetCoverageByAreaResponse(coverage);

        return await Task.FromResult(Result<GetCoverageByAreaResponse>.Success(response));
    }
}
