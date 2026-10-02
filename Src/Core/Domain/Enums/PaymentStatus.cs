namespace SwaOlova.Domain.Enums
{
    public enum PaymentStatus
    {
        None = 0,
        Pending,
        Authorized,
        Paid,
        Failed,
        Cancelled,
        Refunded
    }
}
