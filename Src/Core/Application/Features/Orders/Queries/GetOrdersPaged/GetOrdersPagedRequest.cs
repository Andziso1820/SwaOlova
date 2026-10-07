namespace SwaOlova.Application.Features.Orders.Queries.GetOrdersPaged;

public sealed record GetOrdersPagedRequest(int PageNumber, int PageSize, string? SearchTerm = null);
