using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Notifications.Commands.SendWhatsApp;

public sealed record SendWhatsAppCommand(SendWhatsAppRequest Request)
    : CommandBase<SendWhatsAppResponse>;
