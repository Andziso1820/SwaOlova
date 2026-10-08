using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Rider
{
    public class Rider : AggregateRoot<Guid>
    {
        public string RiderNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string DriversLicenseNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public RiderStatus Status { get; set; }

        public string? StatusReason { get; set; }

        public ICollection<RiderDocument> Documents { get; set; }
            = new List<RiderDocument>();

        public ICollection<RiderActivity> Activities { get; set; }
            = new List<RiderActivity>();

        public static bool CanApprove(RiderStatus status) => status == RiderStatus.PendingApproval;

        public static bool CanSuspend(RiderStatus status) => status is RiderStatus.PendingApproval or RiderStatus.Available or RiderStatus.Offline or RiderStatus.Busy;

        public static bool CanReactivate(RiderStatus status) => status == RiderStatus.Suspended;

        public static bool CanChangeAvailability(RiderStatus status) => status is RiderStatus.Available or RiderStatus.Offline;

        public static bool CanTrackLocation(RiderStatus status) => status is RiderStatus.Available or RiderStatus.Offline or RiderStatus.Busy;
    }
}
