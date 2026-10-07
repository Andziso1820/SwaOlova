using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Locations;

public class CoverageArea : AuditableEntity<Guid>
{
    public Guid MerchantId { get; set; }

    public Guid ZoneId { get; set; }

    public Guid? VillageId { get; set; }

    public decimal DeliveryFee { get; set; }

    public int DeliveryTimeMinutes { get; set; }

    public bool IsAvailable { get; set; }
}
