using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Repositories;

namespace SwaOlova.Infrastructure.Data.Repositories.Merchants;

public sealed class MerchantRepository : Repository<Merchant>, SwaOlova.Application.Common.Interfaces.Repositories.IMerchantRepository
{
    public MerchantRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }

    public override async Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Merchants
            .Include(x => x.Products)
            .Include(x => x.ComplianceDocuments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IReadOnlyCollection<Merchant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Merchants
            .AsNoTracking()
            .Include(x => x.Products)
            .Include(x => x.ComplianceDocuments)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Merchant>> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        var query = DbContext.Merchants
            .AsNoTracking()
            .Include(x => x.Products)
            .Include(x => x.ComplianceDocuments)
            .AsQueryable();

        if (string.IsNullOrWhiteSpace(term))
        {
            return await query
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        var searchTerm = term.Trim();
        return await query
            .Where(x =>
                EF.Functions.Like(x.MerchantCode, $"%{searchTerm}%") ||
                EF.Functions.Like(x.Name, $"%{searchTerm}%") ||
                EF.Functions.Like(x.ContactNumber, $"%{searchTerm}%"))
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
