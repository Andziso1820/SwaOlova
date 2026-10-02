using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;
using ProductEntity = SwaOlova.Domain.Product.Product;

namespace SwaOlova.Domain.Merchant
{
    public class Merchant : AggregateRoot<Guid>
    {
        public string MerchantCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public MerchantStatus Status { get; set; }

        public Guid MerchantCategoryId { get; set; }

        public MerchantAddress Address { get; set; } = null!;

        public ICollection<ProductEntity> Products { get; set; }
            = new List<ProductEntity>();

        public ICollection<MerchantComplianceDocument> ComplianceDocuments { get; set; }
            = new List<MerchantComplianceDocument>();
    }
}
