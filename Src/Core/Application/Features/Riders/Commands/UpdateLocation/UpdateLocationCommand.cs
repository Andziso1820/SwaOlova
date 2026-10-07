using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Commands.UpdateLocation;

public sealed record UpdateLocationCommand(Guid RiderId, UpdateLocationRequest Request)
    : CommandBase<UpdateLocationResponse>;
