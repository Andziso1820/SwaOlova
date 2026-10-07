using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerByPhoneNumber;

public sealed class GetCustomerByPhoneNumberQueryHandler(ICustomerRepository customerRepository)
    : IRequestHandler<GetCustomerByPhoneNumberQuery, Result<GetCustomerByPhoneNumberResponse>>
{
    public async Task<Result<GetCustomerByPhoneNumberResponse>> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerByPhoneNumberResponse>.Failure($"Customer with phone number '{request.PhoneNumber}' was not found.");
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

        return Result<GetCustomerByPhoneNumberResponse>.Success(new GetCustomerByPhoneNumberResponse(customerDto));
    }
}
