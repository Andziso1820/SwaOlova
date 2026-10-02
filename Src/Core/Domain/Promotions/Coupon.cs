using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Promotions
{
    public class Coupon : AuditableEntity<Guid>
    {
        public string Code { get; set; } = string.Empty;

        public decimal DiscountAmount { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
