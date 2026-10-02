using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Identity.Commands.RegisterUser;

public sealed record RegisterUserCommand(RegisterUserRequest Request)
    : CommandBase<RegisterUserResponse>;
