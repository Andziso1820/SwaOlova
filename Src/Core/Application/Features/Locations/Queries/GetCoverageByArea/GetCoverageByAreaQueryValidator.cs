using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Queries.GetCoverageByArea;

public sealed class GetCoverageByAreaQueryValidator : AbstractValidator<GetCoverageByAreaQuery>
{
    public GetCoverageByAreaQueryValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.ZoneId).NotEmpty();
    }
}
