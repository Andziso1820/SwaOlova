using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using SwaOlova.Portal.Models;

namespace SwaOlova.Portal.Infrastructure;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var requestId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        logger.LogError(
            exception,
            "Unhandled exception while processing {Method} {Path}. RequestId: {RequestId}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            requestId);

        if (httpContext.Response.HasStarted)
        {
            return false;
        }

        if (IsAjaxOrJsonRequest(httpContext.Request))
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                new AjaxModalResult
                {
                    Succeeded = false,
                    Message = $"An unexpected error occurred while saving your changes. Reference ID: {requestId}."
                },
                cancellationToken);

            return true;
        }

        httpContext.Response.Redirect($"/Home/Error?requestId={Uri.EscapeDataString(requestId)}");
        return true;
    }

    private static bool IsAjaxOrJsonRequest(HttpRequest request)
    {
        if (request.Headers.TryGetValue("X-Requested-With", out var requestedWith)
            && string.Equals(requestedWith, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (request.Headers.Accept.Any(header => header.Contains("application/json", StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        return request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase);
    }
}
