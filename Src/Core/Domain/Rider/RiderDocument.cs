using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Rider
{
    public class RiderDocument : AuditableEntity<Guid>
    {
        public Guid RiderId { get; set; }

        public RiderDocumentType DocumentType { get; set; } = RiderDocumentType.Other;

        public string FileName { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;

        public string? StoredFileName { get; set; }

        public string? ContentType { get; set; }

        public long? FileSize { get; set; }

        public byte[]? FileData { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
