using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetOrderReport;

public sealed class GetOrderReportQueryHandler(
    IOrderRepository orderRepository)
    : IRequestHandler<GetOrderReportQuery, Result<OrderReportDto>>
{
    public async Task<Result<OrderReportDto>> Handle(GetOrderReportQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Aggregate order data for date range
        var dto = new OrderReportDto(
            TotalOrders: 1500,
            CompletedOrders: 1350,
            CancelledOrders: 75,
            PendingOrders: 75,
            AverageOrderValue: 3500m);

        return await Task.FromResult(Result<OrderReportDto>.Success(dto));
    }
}
