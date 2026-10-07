using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SwaOlova.Application.Common.Interfaces.Services;

namespace SwaOlova.Infrastructure.Service.Identity;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId =>
        httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

    public string? UserName =>
        httpContextAccessor.HttpContext?.User?.Identity?.Name
        ?? httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
        ?? httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
}