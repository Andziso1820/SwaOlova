namespace SwaOlova.Application.Features.Identity.Dtos;

public sealed record LoginDto(
    Guid UserId,
    string Email,
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt);

public sealed record RegisterDto(
    Guid UserId,
    string Email,
    string FullName);

public sealed record UserDto(
    Guid Id,
    string Email,
    string FullName,
    string Role,
    bool IsActive);

public sealed record TokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt);
