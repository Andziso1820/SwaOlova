using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Identity.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler
    : IRequestHandler<ChangePasswordCommand, Result>
{
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, this would:
        // 1. Find user by ID
        // 2. Verify current password
        // 3. Hash new password
        // 4. Update user password in database

        return await Task.FromResult(Result.Success());
    }
}
