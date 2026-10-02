using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Locations.Commands.ConfigureCoverage;

public sealed class ConfigureCoverageCommandHandler(
    IMerchantRepository merchantRepository)
    : IRequestHandler<ConfigureCoverageCommand, Result>
{
    public async Task<Result> Handle(ConfigureCoverageCommand request, CancellationToken cancellationToken)
    {
        // Verify merchant exists
        var merchant = await merchantRepository.GetByIdAsync(request.Request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result.Failure($"Merchant with ID '{request.Request.MerchantId}' was not found.");
        }

        // In a real scenario, this would persist coverage configuration to a repository
        // For now, we're just validating the merchant exists

        return Result.Success();
    }
}
