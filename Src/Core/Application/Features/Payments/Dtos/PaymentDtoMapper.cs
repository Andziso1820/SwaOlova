using SwaOlova.Domain.Payment;

namespace SwaOlova.Application.Features.Payments.Dtos;

internal static class PaymentDtoMapper
{
    public static PaymentDto ToDto(Payment payment, IReadOnlyCollection<Refund> refunds)
    {
        var refundDtos = refunds
            .Select(r => new RefundDto(r.Id, r.PaymentId, r.Amount, r.Reason))
            .ToArray();

        return new PaymentDto(
            payment.Id,
            payment.OrderId,
            payment.ExternalReference ?? payment.Id.ToString(),
            payment.Amount,
            payment.PaymentMethod,
            payment.Status,
            payment.ExternalReference,
            null,
            payment.CreatedDate,
            payment.ModifiedDate,
            null,
            refundDtos);
    }

    public static PaymentDto ToDto(Payment payment)
    {
        return new PaymentDto(
            payment.Id,
            payment.OrderId,
            payment.ExternalReference ?? payment.Id.ToString(),
            payment.Amount,
            payment.PaymentMethod,
            payment.Status,
            payment.ExternalReference,
            null,
            payment.CreatedDate,
            payment.ModifiedDate,
            null,
            Array.Empty<RefundDto>());
    }
}
