using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Notifications.Commands.SendEmail;

public sealed record SendEmailCommand(SendEmailRequest Request)
    : CommandBase<SendEmailResponse>;
