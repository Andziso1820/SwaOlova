using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Commands.CloseMerchant;

public sealed record CloseMerchantCommand(
    Guid MerchantId,
    string Reason)
    : CommandBase;