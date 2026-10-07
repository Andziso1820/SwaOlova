using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetDispatcherDashboard;

public sealed class GetDispatcherDashboardQueryHandler(
    IDeliveryRepository deliveryRepository,
    IRiderRepository riderRepository)
    : IRequestHandler<GetDispatcherDashboardQuery, Result<DispatcherDashboardDto>>
{
    public async Task<Result<DispatcherDashboardDto>> Handle(GetDispatcherDashboardQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Aggregate data for dispatcher dashboard
        var dto = new DispatcherDashboardDto(
            PendingDeliveries: 45,
            InProgressDeliveries: 75,
            CompletedDeliveries: 3500,
            AvailableRiders: 250,
            OnTimePercentage: 95.5m);

        return await Task.FromResult(Result<DispatcherDashboardDto>.Success(dto));
    }
}
