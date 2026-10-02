using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Payments.Commands.CapturePayment;

public sealed class CapturePaymentCommandHandler(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CapturePaymentCommand, Result>
{
    public async Task<Result> Handle(CapturePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
            return Result.Failure($"Payment with ID '{request.PaymentId}' was not found.");
        }

        if (payment.Status != PaymentStatus.Pending)
        {
            return Result.Failure($"Only pending payments can be captured. Current status: {payment.Status}");
        }

        payment.ExternalReference = request.Request.ExternalReference.Trim();
        payment.Status = PaymentStatus.Authorized;

        await paymentRepository.UpdateAsync(payment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
