using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetRevenueReport;

public sealed class GetRevenueReportQueryHandler(
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository)
    : IRequestHandler<GetRevenueReportQuery, Result<GetRevenueReportResponse>>
{
    public async Task<Result<GetRevenueReportResponse>> Handle(GetRevenueReportQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, aggregate payment data for date range
        var revenueData = new List<RevenueReportDto>
        {
            new RevenueReportDto(
                ReportDate: request.StartDate,
                TotalRevenue: 5000m,
                CommissionCollected: 500m,
                NetRevenue: 4500m,
                OrderCount: 150)
        };

        var response = new GetRevenueReportResponse(revenueData.AsReadOnly());
        return await Task.FromResult(Result<GetRevenueReportResponse>.Success(response));
    }
}
