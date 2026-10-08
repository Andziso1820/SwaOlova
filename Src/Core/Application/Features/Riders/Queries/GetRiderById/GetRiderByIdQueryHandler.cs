using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderById;

public sealed record GetRiderByIdResponse(RiderDto Rider);

public sealed class GetRiderByIdQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<GetRiderByIdQuery, Result<GetRiderByIdResponse>>
{
    public async Task<Result<GetRiderByIdResponse>> Handle(GetRiderByIdQuery request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<GetRiderByIdResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        var vehicle = await riderRepository.GetVehicleAsync(rider.Id, cancellationToken);
        var location = await riderRepository.GetLatestLocationAsync(rider.Id, cancellationToken);

        return Result<GetRiderByIdResponse>.Success(new GetRiderByIdResponse(RiderDtoMapper.ToDto(rider, vehicle, location)));
    }
}
