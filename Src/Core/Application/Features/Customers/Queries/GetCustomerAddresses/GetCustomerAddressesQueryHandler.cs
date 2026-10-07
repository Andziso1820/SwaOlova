using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerAddresses;

public sealed class GetCustomerAddressesQueryHandler(ICustomerRepository customerRepository)
    : IRequestHandler<GetCustomerAddressesQuery, Result<GetCustomerAddressesResponse>>
{
    public async Task<Result<GetCustomerAddressesResponse>> Handle(GetCustomerAddressesQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<GetCustomerAddressesResponse>.Failure($"Customer with ID '{request.CustomerId}' was not found.");
        }

        var addresses = await customerRepository.GetAddressesAsync(request.CustomerId, cancellationToken);

        var addressDtos = addresses
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
            .ToArray();

        return Result<GetCustomerAddressesResponse>.Success(new GetCustomerAddressesResponse(request.CustomerId, addressDtos));
    }
}
