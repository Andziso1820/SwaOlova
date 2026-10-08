using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Extensions;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.AddRiderDocument;

public sealed record AddRiderDocumentRequest(
    RiderDocumentType DocumentType,
    string StoredFileName,
    string ContentType,
    long FileSize,
    byte[] FileData,
    DateTime? ExpiryDate);

public sealed record AddRiderDocumentResponse(RiderDocumentDto Document);

public sealed record AddRiderDocumentCommand(Guid RiderId, AddRiderDocumentRequest Request)
    : CommandBase<AddRiderDocumentResponse>;

public sealed class AddRiderDocumentCommandValidator : AbstractValidator<AddRiderDocumentCommand>
{
    public const long MaxFileSizeBytes = 10 * 1024 * 1024;

    public AddRiderDocumentCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.DocumentType)
                .IsInEnum()
                .NotEqual(RiderDocumentType.None)
                .WithMessage("Please select a valid document type.");
            RuleFor(x => x.Request.StoredFileName).NotEmpty().MaximumLength(260);
            RuleFor(x => x.Request.ContentType).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Request.FileSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxFileSizeBytes)
                .WithMessage("Documents must be smaller than 10 MB.");
            RuleFor(x => x.Request.FileData)
                .NotNull()
                .Must(file => file is { Length: > 0 })
                .WithMessage("Uploaded document is required.");
        });
    }
}

public sealed class AddRiderDocumentCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddRiderDocumentCommand, Result<AddRiderDocumentResponse>>
{
    public async Task<Result<AddRiderDocumentResponse>> Handle(AddRiderDocumentCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<AddRiderDocumentResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        var input = request.Request;
        var documentTypeName = input.DocumentType.GetDisplayName();
        var documentId = Guid.NewGuid();

        var document = new RiderDocument
        {
            Id = documentId,
            RiderId = rider.Id,
            DocumentType = input.DocumentType,
            FileName = documentTypeName,
            FileUrl = $"rider-documents/{documentId}",
            StoredFileName = Path.GetFileName(input.StoredFileName),
            ContentType = input.ContentType,
            FileSize = input.FileSize,
            FileData = input.FileData,
            ExpiryDate = input.ExpiryDate?.Date
        };

        await riderRepository.AddDocumentAsync(document, cancellationToken);

        await RiderActivityRecorder.RecordAsync(
            riderRepository,
            rider.Id,
            RiderActivityType.DocumentAttached,
            "Document added",
            $"Added rider document '{documentTypeName}'.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AddRiderDocumentResponse>.Success(new AddRiderDocumentResponse(RiderDtoMapper.ToDocumentDto(document)));
    }
}
