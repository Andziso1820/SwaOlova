using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Payment;

namespace SwaOlova.Application.Features.Payments.Commands.RefundPayment;

public sealed class RefundPaymentCommandHandler(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RefundPaymentCommand, Result>
{
    public async Task<Result> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
            return Result.Failure($"Payment with ID '{request.PaymentId}' was not found.");
        }

        if (payment.Status is not (PaymentStatus.Paid or PaymentStatus.Authorized))
        {
            return Result.Failure($"Only paid payments can be refunded. Current status: {payment.Status}");
        }

        if (request.Request.Amount > payment.Amount)
        {
            return Result.Failure("Refund amount cannot exceed payment amount.");
        }

        var refund = new Refund
        {
            Id = Guid.NewGuid(),
            PaymentId = request.PaymentId,
            Amount = request.Request.Amount,
            Reason = request.Request.Reason.Trim()
        };

        // In a real scenario, refunds would be added to a collection or persisted separately
        // For now, we'll update the payment status to Refunded if full refund

        if (request.Request.Amount == payment.Amount)
        {
            payment.Status = PaymentStatus.Refunded;
        }

        await paymentRepository.UpdateAsync(payment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
