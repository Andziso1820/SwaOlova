using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Locations.Commands.ConfigureCoverage;

public sealed record ConfigureCoverageCommand(ConfigureCoverageRequest Request)
    : CommandBase<Unit>;
