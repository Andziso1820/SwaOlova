using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IMerchantRepository : IRepository<Merchant>
{
    Task<IReadOnlyCollection<Merchant>> SearchAsync(string term, CancellationToken cancellationToken = default);
}