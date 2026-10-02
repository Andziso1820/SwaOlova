using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Dispatch
{
    public class DispatchAssignment : AuditableEntity<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid RiderId { get; set; }

        public Guid AssignedByUserId { get; set; }

        public DateTime AssignedDate { get; set; }
    }
}
