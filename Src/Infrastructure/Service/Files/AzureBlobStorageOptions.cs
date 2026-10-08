namespace SwaOlova.Infrastructure.Service.Files;

public sealed class AzureBlobStorageOptions
{
    public const string SectionName = "Storage:AzureBlob";

    public string? ConnectionString { get; set; }

    public string ContainerName { get; set; } = "profile";
}
