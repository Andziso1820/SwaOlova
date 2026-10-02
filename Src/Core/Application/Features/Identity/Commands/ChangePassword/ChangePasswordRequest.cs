namespace SwaOlova.Application.Features.Identity.Commands.ChangePassword;

public sealed record ChangePasswordRequest(
    Guid UserId,
    string CurrentPassword,
    string NewPassword);
