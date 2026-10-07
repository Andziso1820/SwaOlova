using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomersPaged;

public sealed class GetCustomersPagedQueryHandler(ICustomerRepository customerRepository)
    : IRequestHandler<GetCustomersPagedQuery, Result<GetCustomersPagedResponse>>
{
    public async Task<Result<GetCustomersPagedResponse>> Handle(GetCustomersPagedQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.Request.PageNumber <= 0 ? 1 : request.Request.PageNumber;
        var pageSize = request.Request.PageSize <= 0 ? 10 : request.Request.PageSize;

        var customers = await customerRepository.GetPagedAsync(pageNumber, pageSize, request.Request.SearchTerm, cancellationToken);
        var totalCount = await customerRepository.CountAsync(request.Request.SearchTerm, cancellationToken);

        var customerSummaries = customers
            .Select(customer => new CustomerSummaryDto(
                customer.Id,
                customer.CustomerNumber,
                $"{customer.FirstName} {customer.LastName}".Trim(),
                customer.PhoneNumber,
                customer.IsVerified,
                customer.IsActive))
            .ToArray();

        var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new GetCustomersPagedResponse(
            customerSummaries,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);

        return Result<GetCustomersPagedResponse>.Success(response);
    }
}
