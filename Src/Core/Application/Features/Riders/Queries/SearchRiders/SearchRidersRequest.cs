namespace SwaOlova.Application.Features.Riders.Queries.SearchRiders;

public sealed record SearchRidersRequest(
    string? Keyword,
    string? Status,
    int PageNumber = 1,
    int PageSize = 10);
