using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderLocation;

public sealed class GetRiderLocationQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<GetRiderLocationQuery, Result<GetRiderLocationResponse>>
{
    public async Task<Result<GetRiderLocationResponse>> Handle(GetRiderLocationQuery request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<GetRiderLocationResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        // Note: Would need to retrieve latest location from repository
        // For now, returning null location
        RiderLocationDto? locationDto = null;

        return Result<GetRiderLocationResponse>.Success(new GetRiderLocationResponse(locationDto));
    }
}
