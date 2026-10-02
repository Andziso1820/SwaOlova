using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Payments.Dtos;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentById;

public sealed class GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
    : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
    public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
            return Result<PaymentDto>.Failure($"Payment with ID '{request.PaymentId}' was not found.");
        }

        var paymentDto = PaymentDtoMapper.ToDto(payment);
        return Result<PaymentDto>.Success(paymentDto);
    }
}
