using SwaOlova.Domain.Order;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IReadOnlyCollection<Order>> GetByCustomerIdAsync(
        Guid customerId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<int> CountByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
}