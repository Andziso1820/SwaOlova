using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateOrderCommand, Result<UpdateOrderResponse>>
{
    public async Task<Result<UpdateOrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<UpdateOrderResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status != OrderStatus.Pending)
        {
            return Result<UpdateOrderResponse>.Failure("Only pending orders can be updated.");
        }

        order.DeliveryAddressId = request.Request.DeliveryAddressId;
        order.OrderType = request.Request.OrderType;
        order.DeliveryFee = request.Request.DeliveryFee;
        order.Total = order.SubTotal + order.DeliveryFee;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateOrderResponse>.Success(new UpdateOrderResponse(OrderDtoMapper.ToDto(order)));
    }
}
