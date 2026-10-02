namespace SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;

public sealed record AddComplianceDocumentRequest(
    string Name,
    string FileUrl,
    DateTime? ExpiryDate);
