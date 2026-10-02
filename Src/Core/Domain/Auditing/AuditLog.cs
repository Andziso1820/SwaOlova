using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Auditing
{
    public class AuditLog : AggregateRoot<Guid>
    {
        public string EntityName { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string OldValues { get; set; } = string.Empty;

        public string NewValues { get; set; } = string.Empty;
    }
}
