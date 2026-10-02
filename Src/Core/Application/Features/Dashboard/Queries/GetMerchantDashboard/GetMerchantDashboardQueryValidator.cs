using FluentValidation;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetMerchantDashboard;

public sealed class GetMerchantDashboardQueryValidator : AbstractValidator<GetMerchantDashboardQuery>
{
    public GetMerchantDashboardQueryValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
    }
}
