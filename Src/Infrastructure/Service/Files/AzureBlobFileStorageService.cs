using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SwaOlova.Application.Common.Interfaces.Services;

namespace SwaOlova.Infrastructure.Service.Files;

public sealed class AzureBlobFileStorageService(BlobContainerClient containerClient) : IFileStorageService
{
    public Task<string> UploadAsync(string fileName, Stream content, string? contentType = null, CancellationToken cancellationToken = default)
        => UploadAsync(string.Empty, fileName, content, contentType, cancellationToken);

    public async Task<string> UploadAsync(string folderPath, string fileName, Stream content, string? contentType = null, CancellationToken cancellationToken = default)
    {
        var storedFileName = $"{Guid.NewGuid():N}_{Path.GetFileName(fileName)}";
        var blobName = string.IsNullOrWhiteSpace(folderPath)
            ? storedFileName
            : $"{NormalizeFolderPath(folderPath)}/{storedFileName}";

        var blobClient = containerClient.GetBlobClient(blobName);
        var uploadOptions = new BlobUploadOptions();

        if (!string.IsNullOrWhiteSpace(contentType))
        {
            uploadOptions.HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            };
        }

        await blobClient.UploadAsync(content, uploadOptions, cancellationToken);
        return blobClient.Uri.ToString();
    }

    public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        var blobName = ResolveBlobName(path);
        if (string.IsNullOrWhiteSpace(blobName))
        {
            return;
        }

        await containerClient.DeleteBlobIfExistsAsync(blobName, cancellationToken: cancellationToken);
    }

    private string? ResolveBlobName(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        if (!Uri.TryCreate(path, UriKind.Absolute, out var blobUri))
        {
            return path.Trim('/');
        }

        var containerSegment = $"/{containerClient.Name}/";
        var absolutePath = blobUri.AbsolutePath;
        var containerIndex = absolutePath.IndexOf(containerSegment, StringComparison.OrdinalIgnoreCase);
        if (containerIndex < 0)
        {
            return null;
        }

        return Uri.UnescapeDataString(absolutePath[(containerIndex + containerSegment.Length)..]);
    }

    private static string NormalizeFolderPath(string folderPath)
    {
        var sanitizedSegments = folderPath
            .Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(Path.GetFileName)
            .Where(segment => !string.IsNullOrWhiteSpace(segment));

        return string.Join('/', sanitizedSegments);
    }
}
