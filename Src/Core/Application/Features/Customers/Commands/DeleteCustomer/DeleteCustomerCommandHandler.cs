using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Customers.Commands.DeleteCustomer;

public sealed class DeleteCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCustomerCommand, Result<DeleteCustomerResponse>>
{
    public async Task<Result<DeleteCustomerResponse>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return Result<DeleteCustomerResponse>.Failure($"Customer with ID '{request.CustomerId}' was not found.");
        }

        await customerRepository.DeleteAsync(customer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DeleteCustomerResponse>.Success(new DeleteCustomerResponse(request.CustomerId, true));
    }
}