using SwaOlova.Domain.Delivery;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IDeliveryRepository : IRepository<Delivery>
{
    Task<IReadOnlyCollection<(Delivery Delivery, string OrderNumber)>> GetByRiderIdAsync(
        Guid riderId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<int> CountByRiderIdAsync(Guid riderId, CancellationToken cancellationToken = default);
}