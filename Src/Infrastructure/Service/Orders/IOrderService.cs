using SwaOlova.Domain.Order;

namespace SwaOlova.Infrastructure.Service.Orders;

public interface IOrderService
{
    string GenerateOrderReference(int sequence, DateTime? orderDate = null);

    decimal CalculateItemsTotal(IEnumerable<OrderItem> items);

    decimal CalculateOrderTotal(Order order);

    bool Validate(Order order);

    int CalculateEtaMinutes(decimal distanceKm, int preparationTimeMinutes);
}