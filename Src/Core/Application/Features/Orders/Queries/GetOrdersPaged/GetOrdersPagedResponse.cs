namespace SwaOlova.Application.Features.Orders.Queries.GetOrdersPaged;

public sealed record OrderPagedSummaryDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    string CustomerName,
    Guid MerchantId,
    string MerchantName,
    string Status,
    decimal Total,
    DateTime CreatedDate);

public sealed record GetOrdersPagedResponse(
    IReadOnlyCollection<OrderPagedSummaryDto> Orders,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);
