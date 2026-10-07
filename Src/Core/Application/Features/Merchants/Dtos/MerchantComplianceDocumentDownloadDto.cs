namespace SwaOlova.Application.Features.Merchants.Dtos;

public sealed record MerchantComplianceDocumentDownloadDto(
    Guid Id,
    string Name,
    string? StoredFileName,
    string? ContentType,
    byte[]? FileData,
    string FileUrl);
