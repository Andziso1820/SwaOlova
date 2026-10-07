using FluentValidation;

namespace SwaOlova.Application.Features.Products.Queries.GetProductsByMerchant;

public sealed class GetProductsByMerchantQueryValidator : AbstractValidator<GetProductsByMerchantQuery>
{
    public GetProductsByMerchantQueryValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();

        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
