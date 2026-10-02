using FluentValidation;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetRiderDashboard;

public sealed class GetRiderDashboardQueryValidator : AbstractValidator<GetRiderDashboardQuery>
{
    public GetRiderDashboardQueryValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
    }
}
