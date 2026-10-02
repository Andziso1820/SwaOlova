using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Order;

namespace SwaOlova.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IMerchantRepository merchantRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{
    public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<CreateOrderResponse>.Failure($"Customer with ID '{request.Request.CustomerId}' was not found.");
        }

        var merchant = await merchantRepository.GetByIdAsync(request.Request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<CreateOrderResponse>.Failure($"Merchant with ID '{request.Request.MerchantId}' was not found.");
        }

        var orderItems = new List<OrderItem>();
        foreach (var item in request.Request.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<CreateOrderResponse>.Failure($"Product with ID '{item.ProductId}' was not found.");
            }

            if (product.MerchantId != request.Request.MerchantId)
            {
                return Result<CreateOrderResponse>.Failure($"Product with ID '{item.ProductId}' does not belong to merchant '{request.Request.MerchantId}'.");
            }

            orderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                TotalPrice = product.Price * item.Quantity
            });
        }

        var subTotal = orderItems.Sum(x => x.TotalPrice);
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            CustomerId = request.Request.CustomerId,
            MerchantId = request.Request.MerchantId,
            DeliveryAddressId = request.Request.DeliveryAddressId,
            OrderType = request.Request.OrderType,
            Status = OrderStatus.Pending,
            SubTotal = subTotal,
            DeliveryFee = request.Request.DeliveryFee,
            Total = subTotal + request.Request.DeliveryFee,
            Items = orderItems
        };

        foreach (var item in order.Items)
        {
            item.OrderId = order.Id;
        }

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateOrderResponse>.Success(new CreateOrderResponse(OrderDtoMapper.ToDto(order)));
    }
}
