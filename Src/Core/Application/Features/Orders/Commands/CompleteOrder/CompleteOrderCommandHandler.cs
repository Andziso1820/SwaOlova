using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.CompleteOrder;

public sealed class CompleteOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CompleteOrderCommand, Result<CompleteOrderResponse>>
{
    public async Task<Result<CompleteOrderResponse>> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<CompleteOrderResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status != OrderStatus.Assigned)
        {
            return Result<CompleteOrderResponse>.Failure("Only assigned orders can be completed.");
        }

        order.Status = OrderStatus.Delivered;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CompleteOrderResponse>.Success(new CompleteOrderResponse(OrderDtoMapper.ToDto(order)));
    }
}
