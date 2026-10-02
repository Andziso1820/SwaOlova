using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrderTracking;

public sealed class GetOrderTrackingQueryHandler(
    IOrderRepository orderRepository,
    IDeliveryRepository deliveryRepository,
    IRiderRepository riderRepository,
    IMerchantRepository merchantRepository,
    ICustomerRepository customerRepository)
    : IRequestHandler<GetOrderTrackingQuery, Result<GetOrderTrackingResponse>>
{
    public async Task<Result<GetOrderTrackingResponse>> Handle(GetOrderTrackingQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<GetOrderTrackingResponse>.Failure($"Order with ID '{request.OrderId}' was not found.");
        }

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

        return Result<GetOrderTrackingResponse>.Success(new GetOrderTrackingResponse(tracking));
    }
}
