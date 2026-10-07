using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Product
{
    public class Product : AggregateRoot<Guid>
    {
        public Guid MerchantId { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int PreparationTimeMinutes { get; set; }

        public ProductStatus Status { get; set; }
    }
}
