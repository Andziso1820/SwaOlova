namespace SwaOlova.Application.Features.Identity.Commands.ResetPassword;

public sealed record ResetPasswordRequest(
    string Email,
    string ResetToken,
    string NewPassword);
