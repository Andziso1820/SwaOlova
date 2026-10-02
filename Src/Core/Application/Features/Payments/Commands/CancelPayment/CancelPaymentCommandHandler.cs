using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Payments.Commands.CancelPayment;

public sealed class CancelPaymentCommandHandler(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CancelPaymentCommand, Result>
{
    public async Task<Result> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
            return Result.Failure($"Payment with ID '{request.PaymentId}' was not found.");
        }

        if (payment.Status is PaymentStatus.Paid or PaymentStatus.Refunded or PaymentStatus.Failed or PaymentStatus.Cancelled)
        {
            return Result.Failure($"Cannot cancel payment with status {payment.Status}.");
        }

        payment.Status = PaymentStatus.Cancelled;

        await paymentRepository.UpdateAsync(payment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
