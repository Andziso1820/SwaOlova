using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantComplianceDocumentById;

public sealed record GetMerchantComplianceDocumentByIdQuery(Guid DocumentId)
    : QueryBase<MerchantComplianceDocumentDownloadDto>;
