using Microsoft.EntityFrameworkCore;
using SwaOlova.Application.Common.Interfaces.Services;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Repositories;

namespace SwaOlova.Infrastructure.Data.Repositories.Merchants;

public sealed class MerchantRepository : Repository<Merchant>, SwaOlova.Application.Common.Interfaces.Repositories.IMerchantRepository
{
    private readonly ICurrentUserService _currentUserService;

    public MerchantRepository(SwaOlavaDbContext dbContext, ICurrentUserService currentUserService)
        : base(dbContext)
    {
        _currentUserService = currentUserService;
    }

    public override async Task<Merchant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var merchant = await DbContext.Merchants
            .Include(x => x.Products)
            .Include(x => x.ComplianceDocuments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (merchant is not null)
        {
            await LoadMerchantCategoriesAsync([merchant], cancellationToken);
        }

        return merchant;
    }

    public override async Task<IReadOnlyCollection<Merchant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var merchants = await DbContext.Merchants
            .AsNoTracking()
            //.Include(x => x.ComplianceDocuments)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        await LoadMerchantCategoriesAsync(merchants, cancellationToken);

        return merchants;
    }

    public async Task<IReadOnlyCollection<Merchant>> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        var query = DbContext.Merchants
            .AsNoTracking()
            .Include(x => x.ComplianceDocuments)
            .AsQueryable();

        if (string.IsNullOrWhiteSpace(term))
        {
            var merchants = await query
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            await LoadMerchantCategoriesAsync(merchants, cancellationToken);

            return merchants;
        }

        var searchTerm = term.Trim();
        var searchResults = await query
            .Where(x =>
                EF.Functions.Like(x.MerchantCode, $"%{searchTerm}%") ||
                EF.Functions.Like(x.Name, $"%{searchTerm}%") ||
                EF.Functions.Like(x.ContactNumber, $"%{searchTerm}%"))
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        await LoadMerchantCategoriesAsync(searchResults, cancellationToken);

        return searchResults;
    }

    public async Task<IReadOnlyCollection<MerchantActivity>> GetActivityHistoryAsync(Guid merchantId, CancellationToken cancellationToken = default)
    {
        return await DbContext.MerchantActivities
            .AsNoTracking()
            .Where(x => x.MerchantId == merchantId)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddActivityAsync(MerchantActivity activity, CancellationToken cancellationToken = default)
    {
        activity.CreatedDate = activity.CreatedDate == default
            ? DateTime.UtcNow
            : activity.CreatedDate;

        activity.CreatedBy = GetCurrentUserName();

        await DbContext.MerchantActivities.AddAsync(activity, cancellationToken);
    }

    public async Task<MerchantComplianceDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        return await DbContext.MerchantComplianceDocuments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == documentId, cancellationToken);
    }

    public async Task AddComplianceDocumentAsync(MerchantComplianceDocument document, CancellationToken cancellationToken = default)
    {
        await DbContext.MerchantComplianceDocuments.AddAsync(document, cancellationToken);
    }

    private string GetCurrentUserName()
    {
        return _currentUserService.UserName
            ?? _currentUserService.UserId
            ?? "system";
    }

    private async Task LoadMerchantCategoriesAsync(IReadOnlyCollection<Merchant> merchants, CancellationToken cancellationToken)
    {
        var categoryIds = merchants
            .Select(x => x.MerchantCategoryId)
            .Distinct()
            .ToArray();

        if (categoryIds.Length == 0)
        {
            return;
        }

        var categories = await DbContext.MerchantCategories
            .AsNoTracking()
            .Where(x => categoryIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        foreach (var merchant in merchants)
        {
            if (categories.TryGetValue(merchant.MerchantCategoryId, out var category))
            {
                merchant.MerchantCategory = category;
            }
        }
    }
}
