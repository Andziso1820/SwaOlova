using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Order;

namespace SwaOlova.Application.Features.Orders.Commands.AddOrderItem;

public sealed class AddOrderItemCommandHandler(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddOrderItemCommand, Result<AddOrderItemResponse>>
{
    public async Task<Result<AddOrderItemResponse>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<AddOrderItemResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

        if (order.Status != OrderStatus.Pending)
        {
            return Result<AddOrderItemResponse>.Failure("Items can only be added to pending orders.");
        }

        var product = await productRepository.GetByIdAsync(request.Request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result<AddOrderItemResponse>.Failure($"Product with ID '{request.Request.ProductId}' was not found.");
        }

        if (product.MerchantId != order.MerchantId)
        {
            return Result<AddOrderItemResponse>.Failure($"Product with ID '{request.Request.ProductId}' does not belong to merchant '{order.MerchantId}'.");
        }

        var existingItem = order.Items.FirstOrDefault(item => item.ProductId == product.Id);
        if (existingItem is not null)
        {
            existingItem.Quantity += request.Request.Quantity;
            existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
        }
        else
        {
            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = request.Request.Quantity,
                UnitPrice = product.Price,
                TotalPrice = product.Price * request.Request.Quantity
            });
        }

        order.SubTotal = order.Items.Sum(item => item.TotalPrice);
        order.Total = order.SubTotal + order.DeliveryFee;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AddOrderItemResponse>.Success(new AddOrderItemResponse(OrderDtoMapper.ToDto(order)));
    }
}
