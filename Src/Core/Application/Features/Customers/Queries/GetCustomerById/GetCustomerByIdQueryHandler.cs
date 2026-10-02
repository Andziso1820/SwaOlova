using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdResponse>>
{
    public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerByIdResponse>.Failure($"Customer with ID '{request.CustomerId}' was not found.");
        }

        var customerDto = new CustomerDto(
            customer.Id,
            customer.CustomerNumber,
            customer.FirstName,
            customer.LastName,
            customer.PhoneNumber,
            customer.AlternativePhoneNumber,
            customer.EmailAddress,
            customer.EmailAddress,
            customer.IsVerified,
            customer.IsActive,
            customer.CreatedDate,
            customer.ModifiedDate,
            customer.Addresses
                .Select(address => new CustomerAddressDto(
                    address.Id,
                    address.CustomerId,
                    address.AddressLine1,
                    address.Village,
                    address.Landmark,
                    address.Latitude,
                    address.Longitude,
                    address.GatePhotoUrl,
                    address.IsDefault))
                .ToArray());

        return Result<GetCustomerByIdResponse>.Success(new GetCustomerByIdResponse(customerDto));
    }
}
