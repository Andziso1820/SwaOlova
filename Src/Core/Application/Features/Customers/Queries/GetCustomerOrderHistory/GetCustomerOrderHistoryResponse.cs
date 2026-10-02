namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerOrderHistory;

public sealed record GetCustomerOrderHistoryResponse(
    Guid CustomerId,
    IReadOnlyCollection<CustomerOrderHistoryItemDto> Orders,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);
