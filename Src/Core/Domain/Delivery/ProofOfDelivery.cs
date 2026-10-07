using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Delivery
{
    public class ProofOfDelivery : AuditableEntity<Guid>
    {
        public Guid DeliveryId { get; set; }

        public string? OtpCode { get; set; }

        public string? PhotoUrl { get; set; }

        public string? SignatureUrl { get; set; }
    }
}
