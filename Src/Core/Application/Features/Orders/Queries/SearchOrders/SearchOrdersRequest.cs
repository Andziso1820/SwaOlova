namespace SwaOlova.Application.Features.Orders.Queries.SearchOrders;

public sealed record SearchOrdersRequest(
    string? Keyword,
    string? OrderNumber,
    string? Status,
    int PageNumber = 1,
    int PageSize = 10);
