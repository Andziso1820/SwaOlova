using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Commands.CancelOrder;
using SwaOlova.Application.Features.Orders.Commands.CompleteOrder;
using SwaOlova.Application.Features.Orders.Commands.CreateOrder;
using SwaOlova.Application.Features.Orders.Dtos;
using SwaOlova.Domain.Delivery;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Order;

namespace SwaOlova.Application.Features.Orders.Services;

public sealed class OrderOrchestrator(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IMerchantRepository merchantRepository,
    IProductRepository productRepository,
    IRiderRepository riderRepository,
    IDeliveryRepository deliveryRepository,
    IPromotionRepository promotionRepository,
    IUnitOfWork unitOfWork)
    : IOrderOrchestrator
{
    public async Task<Result<Guid>> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer is null)
                return Result<Guid>.Failure($"Customer with ID '{request.CustomerId}' not found.");

            var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
            if (merchant is null)
                return Result<Guid>.Failure($"Merchant with ID '{request.MerchantId}' not found.");

            if (request.Items.Count == 0)
                return Result<Guid>.Failure("At least one item is required.");

            var orderItems = new List<OrderItem>();
            decimal subTotal = 0;

            foreach (var item in request.Items)
            {
                var product = await productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product is null)
                    return Result<Guid>.Failure($"Product with ID '{item.ProductId}' not found.");

                if (product.MerchantId != request.MerchantId)
                    return Result<Guid>.Failure($"Product '{item.ProductId}' does not belong to merchant '{request.MerchantId}'.");

                var totalPrice = product.Price * item.Quantity;
                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = totalPrice
                });

                subTotal += totalPrice;
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
                CustomerId = request.CustomerId,
                MerchantId = request.MerchantId,
                DeliveryAddressId = request.DeliveryAddressId,
                OrderType = request.OrderType,
                Status = OrderStatus.Pending,
                SubTotal = subTotal,
                DeliveryFee = request.DeliveryFee,
                Total = subTotal + request.DeliveryFee,
                Items = orderItems
            };

            foreach (var item in order.Items)
            {
                item.OrderId = order.Id;
            }

            await orderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(order.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error creating order: {ex.Message}");
        }
    }

    public async Task<Result> ConfirmOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order is null)
                return Result.Failure($"Order with ID '{orderId}' not found.");

            if (order.Status != OrderStatus.Pending)
                return Result.Failure("Only pending orders can be confirmed.");

            order.Status = OrderStatus.Confirmed;
            await orderRepository.UpdateAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error confirming order: {ex.Message}");
        }
    }

    public async Task<Result> CancelOrderAsync(
        CancelOrderRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure($"Order with ID '{request.OrderId}' not found.");

            if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
                return Result.Failure("Cannot cancel delivered or already cancelled orders.");

            order.Status = OrderStatus.Cancelled;
            await orderRepository.UpdateAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error cancelling order: {ex.Message}");
        }
    }

    public async Task<Result> CompleteOrderAsync(
        CompleteOrderRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure($"Order with ID '{request.OrderId}' not found.");

            if (order.Status != OrderStatus.Assigned)
                return Result.Failure("Only assigned orders can be completed.");

            order.Status = OrderStatus.Delivered;
            await orderRepository.UpdateAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error completing order: {ex.Message}");
        }
    }

    public async Task<Result<OrderTrackingDto>> GetTrackingAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order is null)
                return Result<OrderTrackingDto>.Failure($"Order with ID '{orderId}' not found.");

            var delivery = await deliveryRepository.GetByIdAsync(Guid.Empty, cancellationToken);
            var rider = delivery?.RiderId != Guid.Empty ? await riderRepository.GetByIdAsync(delivery!.RiderId, cancellationToken) : null;
            var merchant = await merchantRepository.GetByIdAsync(order.MerchantId, cancellationToken);
            var customer = await customerRepository.GetByIdAsync(order.CustomerId, cancellationToken);

            var tracking = new OrderTrackingDto(
                order.Id,
                order.OrderNumber,
                order.Status,
                delivery?.Status,
                rider?.Id,
                rider != null ? $"{rider.FirstName} {rider.LastName}".Trim() : null,
                rider?.PhoneNumber,
                delivery?.PickupTime,
                delivery?.DeliveryTime,
                delivery?.DistanceKm ?? 0,
                delivery?.EstimatedDurationMinutes ?? 0,
                merchant?.Id ?? Guid.Empty,
                merchant?.Name ?? string.Empty,
                customer?.Id ?? Guid.Empty,
                customer != null ? $"{customer.FirstName} {customer.LastName}".Trim() : string.Empty,
                order.Total);

            return Result<OrderTrackingDto>.Success(tracking);
        }
        catch (Exception ex)
        {
            return Result<OrderTrackingDto>.Failure($"Error retrieving tracking: {ex.Message}");
        }
    }
}
