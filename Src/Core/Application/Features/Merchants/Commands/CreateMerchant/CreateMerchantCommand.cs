using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Commands.CreateMerchant;

public sealed record CreateMerchantCommand(CreateMerchantRequest Request)
    : CommandBase<CreateMerchantResponse>;
