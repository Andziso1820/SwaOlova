using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetFinancialSummary;

public sealed class GetFinancialSummaryQueryHandler(
    IPaymentRepository paymentRepository)
    : IRequestHandler<GetFinancialSummaryQuery, Result<FinancialSummaryDto>>
{
    public async Task<Result<FinancialSummaryDto>> Handle(GetFinancialSummaryQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Calculate financial summary
        var totalIncome = 100000m;
        var totalExpenses = 60000m;
        var netProfit = totalIncome - totalExpenses;

        var dto = new FinancialSummaryDto(
            TotalIncome: totalIncome,
            TotalExpenses: totalExpenses,
            NetProfit: netProfit,
            ProfitMargin: (netProfit / totalIncome) * 100);

        return await Task.FromResult(Result<FinancialSummaryDto>.Success(dto));
    }
}
