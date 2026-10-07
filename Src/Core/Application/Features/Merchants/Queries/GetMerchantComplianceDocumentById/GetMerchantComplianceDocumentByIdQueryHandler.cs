using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantComplianceDocumentById;

public sealed class GetMerchantComplianceDocumentByIdQueryHandler(IMerchantRepository merchantRepository)
    : IRequestHandler<GetMerchantComplianceDocumentByIdQuery, Result<MerchantComplianceDocumentDownloadDto>>
{
    public async Task<Result<MerchantComplianceDocumentDownloadDto>> Handle(
        GetMerchantComplianceDocumentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var document = await merchantRepository.GetDocumentByIdAsync(request.DocumentId, cancellationToken);
        if (document is null)
        {
            return Result<MerchantComplianceDocumentDownloadDto>.Failure($"Compliance document with ID '{request.DocumentId}' was not found.");
        }

        var dto = new MerchantComplianceDocumentDownloadDto(
            document.Id,
            document.Name,
            document.StoredFileName,
            document.ContentType,
            document.FileData,
            document.FileUrl);

        return Result<MerchantComplianceDocumentDownloadDto>.Success(dto);
    }
}
