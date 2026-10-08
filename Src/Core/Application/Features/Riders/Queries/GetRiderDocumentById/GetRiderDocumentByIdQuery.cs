using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderDocumentById;

public sealed record GetRiderDocumentByIdQuery(Guid DocumentId)
    : QueryBase<RiderDocumentDownloadDto>;

public sealed class GetRiderDocumentByIdQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<GetRiderDocumentByIdQuery, Result<RiderDocumentDownloadDto>>
{
    public async Task<Result<RiderDocumentDownloadDto>> Handle(GetRiderDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await riderRepository.GetDocumentByIdAsync(request.DocumentId, cancellationToken);
        if (document is null)
        {
            return Result<RiderDocumentDownloadDto>.Failure("Rider document was not found.");
        }

        return Result<RiderDocumentDownloadDto>.Success(new RiderDocumentDownloadDto(
            document.Id,
            document.RiderId,
            document.FileName,
            document.StoredFileName,
            document.ContentType,
            document.FileData,
            document.FileUrl));
    }
}
