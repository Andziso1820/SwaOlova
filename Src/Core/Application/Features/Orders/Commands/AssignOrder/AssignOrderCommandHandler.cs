using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.AssignOrder;

public sealed class AssignOrderCommandHandler(
    IOrderRepository orderRepository,
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AssignOrderCommand, Result<AssignOrderResponse>>
{
    public async Task<Result<AssignOrderResponse>> Handle(AssignOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<AssignOrderResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status != OrderStatus.Confirmed)
        {
            return Result<AssignOrderResponse>.Failure("Only confirmed orders can be assigned.");
        }

        var rider = await riderRepository.GetByIdAsync(request.Request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<AssignOrderResponse>.Failure($"Rider with ID '{request.Request.RiderId}' was not found.");
        }

        order.Status = OrderStatus.Assigned;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AssignOrderResponse>.Success(new AssignOrderResponse(OrderDtoMapper.ToDto(order), rider.Id));
    }
}
