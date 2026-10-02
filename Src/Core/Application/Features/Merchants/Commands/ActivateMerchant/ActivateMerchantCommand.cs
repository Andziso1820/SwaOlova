using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Commands.ActivateMerchant;

public sealed record ActivateMerchantCommand(
    Guid MerchantId,
    string Reason)
    : CommandBase;
