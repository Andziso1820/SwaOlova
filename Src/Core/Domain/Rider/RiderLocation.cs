using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Rider
{
    public class RiderLocation : AuditableEntity<Guid>
    {
        public Guid RiderId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}
