using System.Text.Json;
using SwaOlova.Application.Common.Interfaces.Services;

namespace SwaOlova.Infrastructure.Service.Reporting;

public sealed class ReportService : IReportService
{
    public Task<byte[]> GenerateAsync(string reportName, object payload, CancellationToken cancellationToken = default)
    {
        var result = new
        {
            ReportName = reportName,
            GeneratedAt = DateTime.UtcNow,
            Payload = payload
        };

        return Task.FromResult(JsonSerializer.SerializeToUtf8Bytes(result));
    }
}