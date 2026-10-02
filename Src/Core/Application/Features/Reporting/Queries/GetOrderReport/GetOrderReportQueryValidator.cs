using FluentValidation;

namespace SwaOlova.Application.Features.Reporting.Queries.GetOrderReport;

public sealed class GetOrderReportQueryValidator : AbstractValidator<GetOrderReportQuery>
{
    public GetOrderReportQueryValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be greater than or equal to start date");
    }
}
