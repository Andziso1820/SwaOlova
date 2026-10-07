using FluentValidation;

namespace SwaOlova.Application.Features.Promotions.Queries.GetPromotionById;

public sealed class GetPromotionByIdQueryValidator : AbstractValidator<GetPromotionByIdQuery>
{
    public GetPromotionByIdQueryValidator()
    {
        RuleFor(x => x.PromotionId).NotEmpty();
    }
}
