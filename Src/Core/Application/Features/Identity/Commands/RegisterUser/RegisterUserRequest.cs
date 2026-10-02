namespace SwaOlova.Application.Features.Identity.Commands.RegisterUser;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string FullName,
    string Role);

public sealed record RegisterUserResponse(
    Guid UserId,
    string Email,
    string FullName);
