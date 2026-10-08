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

        var location = await riderRepository.GetLatestLocationAsync(rider.Id, cancellationToken);

        return Result<GetRiderLocationResponse>.Success(
            new GetRiderLocationResponse(location is null ? null : RiderDtoMapper.ToLocationDto(location)));
    }
}
