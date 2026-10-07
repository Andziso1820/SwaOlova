using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Commands.UploadProductImage;

public sealed record UploadProductImageCommand(Guid ProductId, UploadProductImageRequest Request)
    : CommandBase<Unit>;
