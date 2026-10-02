namespace SwaOlova.Application.Features.Merchants.Commands.UpdateBusinessHours;

public sealed record DayHours(
    string Day,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime,
    bool IsClosed);

public sealed record UpdateBusinessHoursRequest(
    IReadOnlyCollection<DayHours> Hours);
