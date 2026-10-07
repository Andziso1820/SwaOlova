using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Locations;

public class Village : AuditableEntity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public Guid ZoneId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool IsActive { get; set; } = true;
}
