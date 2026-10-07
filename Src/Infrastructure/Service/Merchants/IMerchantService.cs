using SwaOlova.Domain.Merchant;

namespace SwaOlova.Infrastructure.Service.Merchants;

public interface IMerchantService
{
    bool IsOpen(Merchant merchant, DateTime? at = null);

    TimeSpan GetPreparationTime(Merchant merchant);

    Task<IReadOnlyCollection<Merchant>> SearchAsync(string term, CancellationToken cancellationToken = default);

    Task<(int TotalMerchants, int ActiveMerchants)> GetStatisticsAsync(CancellationToken cancellationToken = default);
}