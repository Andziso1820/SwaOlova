using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Commands.UpdateCustomerAddress;

public sealed class UpdateCustomerAddressCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCustomerAddressCommand, Result<UpdateCustomerAddressResponse>>
{
    public async Task<Result<UpdateCustomerAddressResponse>> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<UpdateCustomerAddressResponse>.Failure($"Customer with ID '{request.CustomerId}' was not found.");
        }

        var address = await customerRepository.GetAddressByIdAsync(request.CustomerId, request.AddressId, cancellationToken);
        if (address is null)
        {
            return Result<UpdateCustomerAddressResponse>.Failure($"Address with ID '{request.AddressId}' was not found for customer '{request.CustomerId}'.");
        }

        address.AddressLine1 = request.Request.AddressLine1.Trim();
        address.Village = request.Request.Village.Trim();
        address.Landmark = request.Request.Landmark.Trim();
        address.Latitude = request.Request.Latitude;
        address.Longitude = request.Request.Longitude;
        address.GatePhotoUrl = request.Request.GatePhotoUrl?.Trim();
        address.IsDefault = request.Request.IsDefault;

        await customerRepository.UpdateAddressAsync(address, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var addressDto = new CustomerAddressDto(
            address.Id,
            address.CustomerId,
            address.AddressLine1,
            address.Village,
            address.Landmark,
            address.Latitude,
            address.Longitude,
            address.GatePhotoUrl,
            address.IsDefault);

        return Result<UpdateCustomerAddressResponse>.Success(new UpdateCustomerAddressResponse(addressDto));
    }
}