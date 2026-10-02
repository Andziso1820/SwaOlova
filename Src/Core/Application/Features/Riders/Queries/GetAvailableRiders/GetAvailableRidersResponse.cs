namespace SwaOlova.Application.Features.Riders.Queries.GetAvailableRiders;

public sealed record RiderSummaryDto(
    Guid Id,
    string RiderNumber,
    string FullName,
    string PhoneNumber,
    string Status);

public sealed record GetAvailableRidersResponse(
    IReadOnlyCollection<RiderSummaryDto> Riders,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);
