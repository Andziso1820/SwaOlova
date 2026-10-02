using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomersPaged;

public sealed record GetCustomersPagedResponse(
    IReadOnlyCollection<CustomerSummaryDto> Customers,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);
