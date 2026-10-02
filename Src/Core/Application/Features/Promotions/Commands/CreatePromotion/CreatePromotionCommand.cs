using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Promotions.Commands.CreatePromotion;

public sealed record CreatePromotionCommand(CreatePromotionRequest Request)
    : CommandBase<CreatePromotionResponse>;
