using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Commands.UpdateMerchant;

public sealed record UpdateMerchantCommand(Guid MerchantId, UpdateMerchantRequest Request)
    : CommandBase<UpdateMerchantResponse>;
