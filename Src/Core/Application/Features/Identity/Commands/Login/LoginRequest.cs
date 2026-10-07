namespace SwaOlova.Application.Features.Identity.Commands.Login;

public sealed record LoginRequest(
    string Email,
    string Password);

public sealed record LoginResponse(
    Guid UserId,
    string Email,
    string AccessToken,
    string RefreshToken);
