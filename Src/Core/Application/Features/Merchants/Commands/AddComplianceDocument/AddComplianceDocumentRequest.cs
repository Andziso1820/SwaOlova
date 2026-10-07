using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;

public sealed record AddComplianceDocumentRequest(
    MerchantDocumentType DocumentType,
    string StoredFileName,
    string ContentType,
    long FileSize,
    byte[] FileData,
    DateTime? ExpiryDate);
