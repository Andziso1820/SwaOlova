using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerOrderHistory;

public sealed class GetCustomerOrderHistoryQueryHandler(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<GetCustomerOrderHistoryQuery, Result<GetCustomerOrderHistoryResponse>>
{
    public async Task<Result<GetCustomerOrderHistoryResponse>> Handle(GetCustomerOrderHistoryQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerOrderHistoryResponse>.Failure($"Customer with ID '{request.CustomerId}' was not found.");
        }

        var pageNumber = request.Request.PageNumber <= 0 ? 1 : request.Request.PageNumber;
        var pageSize = request.Request.PageSize <= 0 ? 10 : request.Request.PageSize;

        var orders = await orderRepository.GetByCustomerIdAsync(request.CustomerId, pageNumber, pageSize, cancellationToken);
        var totalCount = await orderRepository.CountByCustomerIdAsync(request.CustomerId, cancellationToken);

        var orderItems = orders
            .Select(order => new CustomerOrderHistoryItemDto(
                order.Id,
                order.OrderNumber,
                order.OrderType,
                order.Status,
                order.SubTotal,
                order.DeliveryFee,
                order.Total))
            .ToArray();

        var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new GetCustomerOrderHistoryResponse(
            request.CustomerId,
            orderItems,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);

        return Result<GetCustomerOrderHistoryResponse>.Success(response);
    }
}
