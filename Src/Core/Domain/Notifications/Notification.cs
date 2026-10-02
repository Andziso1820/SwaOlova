using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Notifications
{
    public class Notification : AggregateRoot<Guid>
    {
        public string Recipient { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Channel { get; set; } = string.Empty;

        public bool Sent { get; set; }
    }
}
