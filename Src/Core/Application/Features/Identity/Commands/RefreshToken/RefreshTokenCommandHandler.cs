using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Identity.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, this would:
        // 1. Validate refresh token exists and hasn't expired
        // 2. Generate new access token
        // 3. Optionally rotate refresh token
        // 4. Return new tokens

        var response = new RefreshTokenResponse(
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...", // New JWT
            Guid.NewGuid().ToString());                  // New refresh token

        return await Task.FromResult(Result<RefreshTokenResponse>.Success(response));
    }
}
