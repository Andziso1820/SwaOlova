namespace SwaOlova.Application.Features.Customers.Queries.GetCustomersPaged;

public sealed record GetCustomersPagedRequest(int PageNumber, int PageSize, string? SearchTerm);
