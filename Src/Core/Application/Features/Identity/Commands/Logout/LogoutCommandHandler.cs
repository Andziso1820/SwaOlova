using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Identity.Commands.Logout;

public sealed class LogoutCommandHandler
    : IRequestHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, this would:
        // 1. Invalidate refresh token in database
        // 2. Add access token to blacklist
        // 3. Clear user session

        return await Task.FromResult(Result.Success());
    }
}
