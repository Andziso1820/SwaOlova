using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerOrderHistory;

public sealed record CustomerOrderHistoryItemDto(
    Guid Id,
    string OrderNumber,
    OrderType OrderType,
    OrderStatus Status,
    decimal SubTotal,
    decimal DeliveryFee,
    decimal Total);
