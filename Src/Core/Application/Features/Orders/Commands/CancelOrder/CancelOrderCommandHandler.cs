using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.CancelOrder;

public sealed class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CancelOrderCommand, Result<CancelOrderResponse>>
{
    public async Task<Result<CancelOrderResponse>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<CancelOrderResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
        {
            return Result<CancelOrderResponse>.Failure("Delivered or cancelled orders cannot be cancelled.");
        }

        order.Status = OrderStatus.Cancelled;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CancelOrderResponse>.Success(new CancelOrderResponse(OrderDtoMapper.ToDto(order)));
    }
}
