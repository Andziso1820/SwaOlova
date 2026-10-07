using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;

public sealed record AddComplianceDocumentCommand(Guid MerchantId, AddComplianceDocumentRequest Request)
    : CommandBase<AddComplianceDocumentResponse>;
