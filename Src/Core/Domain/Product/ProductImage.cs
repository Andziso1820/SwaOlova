using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Product
{
    public class ProductImage : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
