using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IMerchantRepository : IRepository<Merchant>
{
    Task<IReadOnlyCollection<Merchant>> SearchAsync(string term, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<MerchantActivity>> GetActivityHistoryAsync(Guid merchantId, CancellationToken cancellationToken = default);

    Task<MerchantComplianceDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default);

    Task AddComplianceDocumentAsync(MerchantComplianceDocument document, CancellationToken cancellationToken = default);

    Task AddActivityAsync(MerchantActivity activity, CancellationToken cancellationToken = default);
}
