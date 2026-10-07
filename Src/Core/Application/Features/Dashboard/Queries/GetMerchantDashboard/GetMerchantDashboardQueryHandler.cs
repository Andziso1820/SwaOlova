using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetMerchantDashboard;

public sealed class GetMerchantDashboardQueryHandler(
    IMerchantRepository merchantRepository,
    IOrderRepository orderRepository,
    IProductRepository productRepository)
    : IRequestHandler<GetMerchantDashboardQuery, Result<MerchantDashboardDto>>
{
    public async Task<Result<MerchantDashboardDto>> Handle(GetMerchantDashboardQuery request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);

        if (merchant is null)
        {
            return Result<MerchantDashboardDto>.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        // Placeholder: Aggregate merchant-specific data
        var dto = new MerchantDashboardDto(
            MerchantId: request.MerchantId,
            MerchantName: merchant.Name,
            TotalOrders: 500,
            PendingOrders: 25,
            TotalRevenue: 100000m,
            AverageRating: 4.5m,
            ActiveProducts: 150);

        return Result<MerchantDashboardDto>.Success(dto);
    }
}
