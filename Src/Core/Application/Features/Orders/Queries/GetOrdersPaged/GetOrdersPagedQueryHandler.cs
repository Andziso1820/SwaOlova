using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrdersPaged;

public sealed class GetOrdersPagedQueryHandler(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IMerchantRepository merchantRepository)
    : IRequestHandler<GetOrdersPagedQuery, Result<GetOrdersPagedResponse>>
{
    public async Task<Result<GetOrdersPagedResponse>> Handle(GetOrdersPagedQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.Request.PageNumber <= 0 ? 1 : request.Request.PageNumber;
        var pageSize = request.Request.PageSize <= 0 ? 10 : request.Request.PageSize;

        var orders = await orderRepository.GetByCustomerIdAsync(Guid.Empty, pageNumber, pageSize, cancellationToken);
        var totalCount = await orderRepository.CountByCustomerIdAsync(Guid.Empty, cancellationToken);

        var orderSummaries = new List<OrderPagedSummaryDto>();

        foreach (var order in orders)
        {
            var customer = await customerRepository.GetByIdAsync(order.CustomerId, cancellationToken);
            var merchant = await merchantRepository.GetByIdAsync(order.MerchantId, cancellationToken);

            orderSummaries.Add(new OrderPagedSummaryDto(
                order.Id,
                order.OrderNumber,
                order.CustomerId,
                customer != null ? $"{customer.FirstName} {customer.LastName}".Trim() : string.Empty,
                order.MerchantId,
                merchant?.Name ?? string.Empty,
                order.Status.ToString(),
                order.Total,
                order.CreatedDate));
        }

        var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new GetOrdersPagedResponse(
            orderSummaries,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);

        return Result<GetOrdersPagedResponse>.Success(response);
    }
}
