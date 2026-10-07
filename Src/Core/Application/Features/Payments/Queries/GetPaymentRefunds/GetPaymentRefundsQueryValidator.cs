using FluentValidation;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentRefunds;

public sealed class GetPaymentRefundsQueryValidator : AbstractValidator<GetPaymentRefundsQuery>
{
    public GetPaymentRefundsQueryValidator()
    {
        RuleFor(x => x.PaymentId).NotEmpty();
    }
}
