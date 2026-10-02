using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;
using SwaOlova.Domain.Customer;

namespace SwaOlova.Application.Features.Customers.Commands.AddCustomerAddress;

public sealed class AddCustomerAddressCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddCustomerAddressCommand, Result<AddCustomerAddressResponse>>
{
    public async Task<Result<AddCustomerAddressResponse>> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<AddCustomerAddressResponse>.Failure($"Customer with ID '{request.CustomerId}' was not found.");
        }

        var address = new CustomerAddress
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            AddressLine1 = request.Request.AddressLine1.Trim(),
            Village = request.Request.Village.Trim(),
            Landmark = request.Request.Landmark.Trim(),
            Latitude = request.Request.Latitude,
            Longitude = request.Request.Longitude,
            GatePhotoUrl = request.Request.GatePhotoUrl?.Trim(),
            IsDefault = request.Request.IsDefault
        };

        await customerRepository.AddAddressAsync(address, cancellationToken);
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

        return Result<AddCustomerAddressResponse>.Success(new AddCustomerAddressResponse(addressDto));
    }
}