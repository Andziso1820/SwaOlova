namespace SwaOlova.Application.Common.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> UploadAsync(string fileName, Stream content, string? contentType = null, CancellationToken cancellationToken = default);

    Task<string> UploadAsync(string folderPath, string fileName, Stream content, string? contentType = null, CancellationToken cancellationToken = default);

    Task DeleteAsync(string path, CancellationToken cancellationToken = default);
}