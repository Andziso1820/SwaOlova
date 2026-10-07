using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Merchant;

public class MerchantActivity : AuditableEntity<Guid>
{
    public Guid MerchantId { get; set; }

    public MerchantActivityType ActivityType { get; set; }

    public string Title { get; set; }

    public string Description { get; set; } = string.Empty;
}
