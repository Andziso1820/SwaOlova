using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Order;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Repositories;

namespace SwaOlova.Infrastructure.Data.Repositories.Orders;

public sealed class OrderRepository : Repository<Order>, SwaOlova.Application.Common.Interfaces.Repositories.IOrderRepository
{
    public OrderRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Order>> GetByCustomerIdAsync(
        Guid customerId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Orders
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.OrderDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Orders.CountAsync(x => x.CustomerId == customerId, cancellationToken);
    }
}
