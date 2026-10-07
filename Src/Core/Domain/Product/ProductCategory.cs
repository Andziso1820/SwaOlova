using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Product
{
    public class ProductCategory : AuditableEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
