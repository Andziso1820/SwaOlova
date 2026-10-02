namespace SwaOlova.Domain.Enums
{
    public enum OrderStatus
    {
        None = 0,
        Pending,
        Confirmed,
        Assigned,
        Collected,
        OnTheWay,
        Delivered,
        Cancelled,
        FailedDelivery
    }
}
