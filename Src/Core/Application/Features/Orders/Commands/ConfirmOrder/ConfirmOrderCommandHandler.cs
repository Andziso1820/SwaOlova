using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.ConfirmOrder;

public sealed class ConfirmOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ConfirmOrderCommand, Result<ConfirmOrderResponse>>
{
    public async Task<Result<ConfirmOrderResponse>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<ConfirmOrderResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status != OrderStatus.Pending)
        {
            return Result<ConfirmOrderResponse>.Failure("Only pending orders can be confirmed.");
        }

        order.Status = OrderStatus.Confirmed;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ConfirmOrderResponse>.Success(new ConfirmOrderResponse(OrderDtoMapper.ToDto(order)));
    }
}
