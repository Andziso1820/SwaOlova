using MediatR;
using Microsoft.Extensions.Logging;
using SwaOlova.Application.Common.Interfaces.Services;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Common.Behaviors;

public sealed class AuditBehavior<TRequest, TResponse>(
    ILogger<AuditBehavior<TRequest, TResponse>> logger,
    ICurrentUserService? currentUserService = null,
    ICurrentTenantService? currentTenantService = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.GetType().Name.EndsWith("Command", StringComparison.Ordinal) && IsSuccessful(response))
        {
            logger.LogInformation(
                "Audited command {RequestName} for user {UserName} in tenant {TenantId}",
                typeof(TRequest).Name,
                currentUserService?.UserName ?? currentUserService?.UserId ?? "anonymous",
                currentTenantService?.TenantId ?? "default");
        }

        return response;
    }

    private static bool IsSuccessful(TResponse response)
        => response switch
        {
            Result result => result.Succeeded,
            _ => true
        };
}
