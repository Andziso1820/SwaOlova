using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrderByIdQuery, Result<GetOrderByIdResponse>>
{
    public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<GetOrderByIdResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        var orderDto = OrderDtoMapper.ToDto(order);
        return Result<GetOrderByIdResponse>.Success(new GetOrderByIdResponse(orderDto));
    }
}
