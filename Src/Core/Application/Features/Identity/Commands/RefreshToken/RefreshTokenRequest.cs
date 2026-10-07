namespace SwaOlova.Application.Features.Identity.Commands.RefreshToken;

public sealed record RefreshTokenRequest(
    string AccessToken,
    string RefreshToken);

public sealed record RefreshTokenResponse(
    string NewAccessToken,
    string NewRefreshToken);
