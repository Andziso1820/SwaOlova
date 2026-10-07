namespace SwaOlova.Application.Common.Interfaces.Services;

public interface IPricingService
{
    Task<decimal> CalculateDeliveryFeeAsync(Guid merchantId, Guid deliveryAddressId, CancellationToken cancellationToken = default);

    Task<decimal> CalculateOrderTotalAsync(Guid merchantId, IEnumerable<(Guid ProductId, int Quantity)> items, CancellationToken cancellationToken = default);
}
