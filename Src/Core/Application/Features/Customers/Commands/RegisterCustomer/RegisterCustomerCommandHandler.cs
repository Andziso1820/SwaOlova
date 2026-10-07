using AutoMapper;
using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Customers.Dtos;
using SwaOlova.Domain.Customer;

namespace SwaOlova.Application.Features.Customers.Commands.RegisterCustomer;

public sealed class RegisterCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<RegisterCustomerCommand, Result<RegisterCustomerResponse>>
{
    public async Task<Result<RegisterCustomerResponse>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await customerRepository.GetByPhoneNumberAsync(request.Request.PhoneNumber, cancellationToken);
        if (existingCustomer is not null)
        {
            return Result<RegisterCustomerResponse>.Failure($"A customer with phone number '{request.Request.PhoneNumber}' already exists.");
        }

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerNumber = $"CUS-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            FirstName = request.Request.FirstName.Trim(),
            LastName = request.Request.LastName.Trim(),
            PhoneNumber = request.Request.PhoneNumber.Trim(),
            AlternativePhoneNumber = request.Request.AlternativePhoneNumber?.Trim(),
            EmailAddress = request.Request.EmailAddress?.Trim(),
            IsVerified = false,
            IsActive = true
        };

        await customerRepository.AddAsync(customer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var customerDto = mapper.Map<CustomerDto>(customer);
        return Result<RegisterCustomerResponse>.Success(new RegisterCustomerResponse(customerDto));
    }
}
