namespace SwaOlova.Application.Common.Interfaces.Services;

public interface IReportService
{
    Task<byte[]> GenerateAsync(string reportName, object payload, CancellationToken cancellationToken = default);
}
