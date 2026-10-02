using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Merchant;

public class MerchantComplianceDocument : AuditableEntity<Guid>
{
    public Guid MerchantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string FileUrl { get; set; } = string.Empty;

    public DateTime? ExpiryDate { get; set; }
}
