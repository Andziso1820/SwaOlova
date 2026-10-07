using FluentValidation;

namespace SwaOlova.Application.Features.Reporting.Queries.GetDeliveryMetrics;

public sealed class GetDeliveryMetricsQueryValidator : AbstractValidator<GetDeliveryMetricsQuery>
{
    public GetDeliveryMetricsQueryValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate);
    }
}
