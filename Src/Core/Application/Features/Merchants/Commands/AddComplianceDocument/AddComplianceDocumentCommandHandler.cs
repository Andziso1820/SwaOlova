using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;

public sealed class AddComplianceDocumentCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddComplianceDocumentCommand, Result<AddComplianceDocumentResponse>>
{
    public async Task<Result<AddComplianceDocumentResponse>> Handle(AddComplianceDocumentCommand request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<AddComplianceDocumentResponse>.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        var document = new MerchantComplianceDocument
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            Name = request.Request.Name.Trim(),
            FileUrl = request.Request.FileUrl.Trim(),
            ExpiryDate = request.Request.ExpiryDate
        };

        merchant.ComplianceDocuments.Add(document);

        await merchantRepository.UpdateAsync(merchant, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var documentDto = new MerchantComplianceDocumentDto(
            document.Id,
            document.MerchantId,
            document.Name,
            document.FileUrl,
            document.ExpiryDate,
            document.CreatedDate);

        return Result<AddComplianceDocumentResponse>.Success(new AddComplianceDocumentResponse(documentDto));
    }
}
