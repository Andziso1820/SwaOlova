using SwaOlova.Application.Common.Interfaces.Services;

namespace SwaOlova.Infrastructure.Service.Files;

public sealed class LocalFileStorageService : IFileStorageService
{
    private const string UploadRoot = "wwwroot/uploads";

    public async Task<string> UploadAsync(string fileName, Stream content, CancellationToken cancellationToken = default)
    {
        var storedFileName = GetStoredFileName(fileName);
        var relativePath = Path.Combine(UploadRoot, storedFileName);
        var fullPath = GetFullPath(relativePath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using var fileStream = File.Create(fullPath);
        await content.CopyToAsync(fileStream, cancellationToken);

        return NormalizePath(relativePath);
    }

    public async Task<Stream> DownloadAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var fullPath = GetFullPath(relativePath);
        return await Task.FromResult<Stream>(File.OpenRead(fullPath));
    }

    public Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var fullPath = GetFullPath(relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    public string GetUrl(string relativePath) => NormalizePath(relativePath);

    private static string GetStoredFileName(string fileName)
    {
        var safeFileName = Path.GetFileName(fileName);
        return $"{Guid.NewGuid():N}_{safeFileName}";
    }

    private static string GetFullPath(string relativePath)
        => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));

    private static string NormalizePath(string path)
        => path.Replace(Path.DirectorySeparatorChar, '/');
}