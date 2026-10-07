using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Identity.Commands.ChangePassword;
using SwaOlova.Application.Features.Identity.Commands.Login;
using SwaOlova.Application.Features.Identity.Commands.Logout;
using SwaOlova.Application.Features.Identity.Commands.RefreshToken;
using SwaOlova.Application.Features.Identity.Commands.RegisterUser;
using SwaOlova.Application.Features.Identity.Commands.ResetPassword;

namespace SwaOlova.Application.Features.Identity.Services;

public sealed class IdentityOrchestrator(IMediator mediator)
    : IIdentityOrchestrator
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var command = new LoginCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result> LogoutAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var command = new LogoutCommand(userId);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var command = new RegisterUserCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var command = new ChangePasswordCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var command = new ResetPasswordCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var command = new RefreshTokenCommand(request);
        return await mediator.Send(command, cancellationToken);
    }
}
