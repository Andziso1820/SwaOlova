using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Commands.SuspendMerchant;

public sealed record SuspendMerchantCommand(
    Guid MerchantId,
    string Reason)
    : CommandBase;