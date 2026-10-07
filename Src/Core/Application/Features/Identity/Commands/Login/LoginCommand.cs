using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Identity.Commands.Login;

public sealed record LoginCommand(LoginRequest Request)
    : CommandBase<LoginResponse>;
