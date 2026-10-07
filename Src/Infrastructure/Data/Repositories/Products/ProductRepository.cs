using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Product;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Repositories;

namespace SwaOlova.Infrastructure.Data.Repositories.Products;

public sealed class ProductRepository : Repository<Product>, SwaOlova.Application.Common.Interfaces.Repositories.IProductRepository
{
    public ProductRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Product>> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return await DbContext.Products.AsNoTracking().OrderBy(x => x.Name).ToListAsync(cancellationToken);
        }

        var searchTerm = term.Trim();
        return await DbContext.Products
            .AsNoTracking()
            .Where(x =>
                EF.Functions.Like(x.Name, $"%{searchTerm}%") ||
                EF.Functions.Like(x.Description, $"%{searchTerm}%"))
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
