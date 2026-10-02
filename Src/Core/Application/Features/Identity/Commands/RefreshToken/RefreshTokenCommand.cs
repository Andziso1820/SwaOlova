using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Identity.Commands.RefreshToken;

public sealed record RefreshTokenCommand(RefreshTokenRequest Request)
    : CommandBase<RefreshTokenResponse>;
