using FluentValidation;

namespace SwaOlova.Application.Features.Payments.Queries.GetPaymentsByOrder;

public sealed class GetPaymentsByOrderQueryValidator : AbstractValidator<GetPaymentsByOrderQuery>
{
    public GetPaymentsByOrderQueryValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
