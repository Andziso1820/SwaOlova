using SwaOlova.Domain.Product;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyCollection<Product>> SearchAsync(string term, CancellationToken cancellationToken = default);
}