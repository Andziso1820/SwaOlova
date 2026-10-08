using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Delivery;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Repositories.Deliveries;

public sealed class DeliveryRepository : Repository<Delivery>, SwaOlova.Application.Common.Interfaces.Repositories.IDeliveryRepository
{
    public DeliveryRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<(Delivery Delivery, string OrderNumber)>> GetByRiderIdAsync(
        Guid riderId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var rows = await (
                from delivery in DbContext.Deliveries.AsNoTracking()
                join order in DbContext.Orders.AsNoTracking() on delivery.OrderId equals order.Id into orders
                from order in orders.DefaultIfEmpty()
                where delivery.RiderId == riderId
                orderby delivery.CreatedDate descending
                select new { Delivery = delivery, OrderNumber = order != null ? order.OrderNumber : string.Empty })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return rows.Select(x => (x.Delivery, x.OrderNumber)).ToArray();
    }

    public async Task<int> CountByRiderIdAsync(Guid riderId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Deliveries.CountAsync(x => x.RiderId == riderId, cancellationToken);
    }
}
