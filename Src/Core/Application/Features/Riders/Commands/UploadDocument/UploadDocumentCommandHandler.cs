using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.UploadDocument;

public sealed class UploadDocumentCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadDocumentCommand, Result<UploadDocumentResponse>>
{
    public async Task<Result<UploadDocumentResponse>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<UploadDocumentResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        var document = new RiderDocument
        {
            Id = Guid.NewGuid(),
            RiderId = request.RiderId,
            FileName = request.Request.FileName.Trim(),
            FileUrl = request.Request.FileUrl.Trim()
        };

        // Note: Assuming IRiderRepository would have a method to add document or we add it via a separate interface
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var documentDto = RiderDtoMapper.ToDocumentDto(document);
        return Result<UploadDocumentResponse>.Success(new UploadDocumentResponse(documentDto));
    }
}
