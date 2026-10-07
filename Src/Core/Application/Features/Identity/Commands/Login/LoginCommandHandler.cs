using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Identity.Commands.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, this would:
        // 1. Validate credentials against user database
        // 2. Hash password comparison
        // 3. Generate JWT tokens
        // 4. Create refresh token in database

        var response = new LoginResponse(
            Guid.NewGuid(),
            request.Request.Email.Trim(),
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...", // Placeholder JWT
            Guid.NewGuid().ToString());                  // Placeholder refresh token

        return await Task.FromResult(Result<LoginResponse>.Success(response));
    }
}
