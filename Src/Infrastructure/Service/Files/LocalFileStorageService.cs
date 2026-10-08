using Microsoft.Extensions.Hosting;
using SwaOlova.Application.Common.Interfaces.Services;

namespace SwaOlova.Infrastructure.Service.Files;

public sealed class LocalFileStorageService(IHostEnvironment environment) : IFileStorageService
{
    private const string UploadRoot = "uploads";

    public Task<string> UploadAsync(string fileName, Stream content, string? contentType = null, CancellationToken cancellationToken = default)
        => UploadAsync(string.Empty, fileName, content, contentType, cancellationToken);

    public async Task<string> UploadAsync(string folderPath, string fileName, Stream content, string? contentType = null, CancellationToken cancellationToken = default)
    {
        var normalizedFolderPath = NormalizeFolderPath(folderPath);
        var storedFileName = GetStoredFileName(fileName);
        var relativePath = BuildRelativePath(normalizedFolderPath, storedFileName);
        var fullPath = GetFullPath(relativePath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using var fileStream = File.Create(fullPath);
        await content.CopyToAsync(fileStream, cancellationToken);

        return NormalizePath(relativePath);
    }

    public Task DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        if (!IsLocalStoragePath(path))
        {
            return Task.CompletedTask;
        }

        var relativePath = path.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.Combine(GetWebRootPath(), relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    private static string GetStoredFileName(string fileName)
    {
        var safeFileName = Path.GetFileName(fileName);
        return $"{Guid.NewGuid():N}_{safeFileName}";
    }

    private string GetFullPath(string relativePath)
        => Path.GetFullPath(Path.Combine(GetWebRootPath(), relativePath));

    private string GetWebRootPath()
        => Path.Combine(environment.ContentRootPath, "wwwroot");

    private static string BuildRelativePath(string folderPath, string storedFileName)
    {
        var segments = new List<string> { UploadRoot };

        if (!string.IsNullOrWhiteSpace(folderPath))
        {
            segments.AddRange(folderPath.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        segments.Add(storedFileName);
        return Path.Combine([.. segments]);
    }

    private static string NormalizeFolderPath(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            return string.Empty;
        }

        var sanitizedSegments = folderPath
            .Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(Path.GetFileName)
            .Where(segment => !string.IsNullOrWhiteSpace(segment));

        return string.Join('/', sanitizedSegments);
    }

    private static bool IsLocalStoragePath(string path)
        => !string.IsNullOrWhiteSpace(path)
           && !Uri.TryCreate(path, UriKind.Absolute, out _)
           && path.StartsWith("/", StringComparison.Ordinal);

    private static string NormalizePath(string path)
        => '/' + path.Replace(Path.DirectorySeparatorChar, '/').TrimStart('/');
}