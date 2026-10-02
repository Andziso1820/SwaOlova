using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Payments.Commands.ProcessWebhook;

public sealed record ProcessWebhookCommand(ProcessWebhookRequest Request)
    : CommandBase<Unit>;
