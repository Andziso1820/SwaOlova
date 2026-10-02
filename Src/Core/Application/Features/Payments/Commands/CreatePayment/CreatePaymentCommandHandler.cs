using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Payments.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Payment;

namespace SwaOlova.Application.Features.Payments.Commands.CreatePayment;

public sealed class CreatePaymentCommandHandler(
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePaymentCommand, Result<CreatePaymentResponse>>
{
    public async Task<Result<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        // Verify order exists
        var order = await orderRepository.GetByIdAsync(request.Request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result<CreatePaymentResponse>.Failure($"Order with ID '{request.Request.OrderId}' was not found.");
        }

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            OrderId = request.Request.OrderId,
            Amount = request.Request.Amount,
            PaymentMethod = request.Request.PaymentMethod.Trim(),
            Status = PaymentStatus.Pending,
            ExternalReference = null
        };

        await paymentRepository.AddAsync(payment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var paymentDto = PaymentDtoMapper.ToDto(payment);
        return Result<CreatePaymentResponse>.Success(new CreatePaymentResponse(paymentDto));
    }
}
