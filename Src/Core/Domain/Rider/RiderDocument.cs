using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Rider
{
    public class RiderDocument : AuditableEntity<Guid>
    {
        public Guid RiderId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;
    }
}
