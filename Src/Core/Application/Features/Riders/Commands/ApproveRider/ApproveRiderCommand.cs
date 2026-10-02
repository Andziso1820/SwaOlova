using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Commands.ApproveRider;

public sealed record ApproveRiderCommand(Guid RiderId)
    : CommandBase<ApproveRiderResponse>;
