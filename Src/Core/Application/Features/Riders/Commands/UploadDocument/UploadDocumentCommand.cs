using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Commands.UploadDocument;

public sealed record UploadDocumentCommand(Guid RiderId, UploadDocumentRequest Request)
    : CommandBase<UploadDocumentResponse>;
