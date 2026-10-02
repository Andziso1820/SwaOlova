using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Payments.Dtos;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentRefunds;

public sealed class GetPaymentRefundsQueryHandler(IPaymentRepository paymentRepository)
    : IRequestHandler<GetPaymentRefundsQuery, Result<GetPaymentRefundsResponse>>
{
    public async Task<Result<GetPaymentRefundsResponse>> Handle(GetPaymentRefundsQuery request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
        if (payment is null)
        {
            return Result<GetPaymentRefundsResponse>.Failure($"Payment with ID '{request.PaymentId}' was not found.");
        }

        // In a real scenario, refunds would be queried from a repository or loaded via navigation property
        // For now, returning empty collection with placeholder:
        var refunds = Array.Empty<RefundDto>();
        var totalRefunded = 0m;

        var response = new GetPaymentRefundsResponse(refunds, totalRefunded);
        return Result<GetPaymentRefundsResponse>.Success(response);
    }
}
