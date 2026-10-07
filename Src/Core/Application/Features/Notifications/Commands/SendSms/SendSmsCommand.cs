using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Notifications.Commands.SendSms;

public sealed record SendSmsCommand(SendSmsRequest Request)
    : CommandBase<SendSmsResponse>;
