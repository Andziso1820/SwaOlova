using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Rider;

public class RiderActivity : AuditableEntity<Guid>
{
    public Guid RiderId { get; set; }

    public RiderActivityType ActivityType { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
