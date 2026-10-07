using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Merchant;

public class MerchantComplianceDocument : AuditableEntity<Guid>
{
    public Guid MerchantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public MerchantDocumentType DocumentType { get; set; } = MerchantDocumentType.Other;

    public string FileUrl { get; set; } = string.Empty;

    public string? StoredFileName { get; set; }

    public string? ContentType { get; set; }

    public long? FileSize { get; set; }

    public byte[]? FileData { get; set; }

    public DateTime? ExpiryDate { get; set; }
}
