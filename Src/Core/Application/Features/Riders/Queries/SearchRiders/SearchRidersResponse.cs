namespace SwaOlova.Application.Features.Riders.Queries.SearchRiders;

public sealed record SearchRiderResult(
    Guid Id,
    string RiderNumber,
    string FullName,
    string PhoneNumber,
    string Status,
    string DriversLicense);

public sealed record SearchRidersResponse(
    IReadOnlyCollection<SearchRiderResult> Results,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);
