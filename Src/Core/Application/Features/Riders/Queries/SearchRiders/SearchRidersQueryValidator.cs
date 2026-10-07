using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Queries.SearchRiders;

public sealed class SearchRidersQueryValidator : AbstractValidator<SearchRidersQuery>
{
    public SearchRidersQueryValidator()
    {
        RuleFor(x => x.Request).NotNull();
        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Request.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
        });
    }
}
