using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantProducts;

public sealed class GetMerchantProductsQueryValidator : AbstractValidator<GetMerchantProductsQuery>
{
    public GetMerchantProductsQueryValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();

        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
