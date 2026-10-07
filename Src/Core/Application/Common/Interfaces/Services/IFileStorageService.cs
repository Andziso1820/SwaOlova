namespace SwaOlova.Application.Common.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> UploadAsync(string fileName, Stream content, CancellationToken cancellationToken = default);
}