using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Locations;

public class Zone : AuditableEntity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<Village> Villages { get; set; } = new List<Village>();

    public ICollection<CoverageArea> CoverageAreas { get; set; } = new List<CoverageArea>();
}
