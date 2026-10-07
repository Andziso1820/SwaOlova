using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Identity.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
{
    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, this would:
        // 1. Check if email already exists
        // 2. Hash password
        // 3. Create user in database
        // 4. Send confirmation email

        var response = new RegisterUserResponse(
            Guid.NewGuid(),
            request.Request.Email.Trim(),
            request.Request.FullName.Trim());

        return await Task.FromResult(Result<RegisterUserResponse>.Success(response));
    }
}
