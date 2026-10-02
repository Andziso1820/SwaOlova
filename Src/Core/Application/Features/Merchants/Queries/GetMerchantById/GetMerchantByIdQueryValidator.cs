using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantById;

public sealed class GetMerchantByIdQueryValidator : AbstractValidator<GetMerchantByIdQuery>
{
    public GetMerchantByIdQueryValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
    }
}
