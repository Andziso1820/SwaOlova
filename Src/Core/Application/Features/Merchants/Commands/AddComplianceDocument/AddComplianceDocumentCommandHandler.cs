using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Interfaces.Services;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Extensions;
using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;

public sealed class AddComplianceDocumentCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<AddComplianceDocumentCommand, Result<AddComplianceDocumentResponse>>
{
    public async Task<Result<AddComplianceDocumentResponse>> Handle(AddComplianceDocumentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(currentUserService.UserName))
        {
            return Result<AddComplianceDocumentResponse>.Failure(
                "User context is required to upload compliance documents.");
        }

        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<AddComplianceDocumentResponse>.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        if (request.Request.FileData.Length == 0)
        {
            return Result<AddComplianceDocumentResponse>.Failure("Uploaded document is empty.");
        }

        var documentTypeName = request.Request.DocumentType.GetDisplayName();

        var document = new MerchantComplianceDocument
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            Name = documentTypeName,
            DocumentType = request.Request.DocumentType,
            FileUrl = string.Empty,
            StoredFileName = request.Request.StoredFileName.Trim(),
            ContentType = string.IsNullOrWhiteSpace(request.Request.ContentType) ? "application/octet-stream" : request.Request.ContentType,
            FileSize = request.Request.FileSize,
            FileData = request.Request.FileData,
            ExpiryDate = request.Request.ExpiryDate,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = currentUserService.UserName
        };

        await merchantRepository.AddComplianceDocumentAsync(document, cancellationToken);

        await merchantRepository.AddActivityAsync(new MerchantActivity
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            ActivityType = MerchantActivityType.DocumentAttached,
            Title = "Compliance document added",
            Description = $"Added compliance document '{documentTypeName}'."
        }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var documentDto = new MerchantComplianceDocumentDto(
            document.Id,
            document.MerchantId,
            document.Name,
            document.DocumentType,
            document.FileUrl,
            document.ExpiryDate,
            document.CreatedDate);

        return Result<AddComplianceDocumentResponse>.Success(new AddComplianceDocumentResponse(documentDto));
    }
}
