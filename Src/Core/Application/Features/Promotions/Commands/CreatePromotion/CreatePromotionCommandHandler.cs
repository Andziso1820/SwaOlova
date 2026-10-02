using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Promotions.Dtos;
using SwaOlova.Domain.Promotions;

namespace SwaOlova.Application.Features.Promotions.Commands.CreatePromotion;

public sealed class CreatePromotionCommandHandler(
    IPromotionRepository promotionRepository)
    : IRequestHandler<CreatePromotionCommand, Result<CreatePromotionResponse>>
{
    public async Task<Result<CreatePromotionResponse>> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = new Promotion
        {
            Id = Guid.NewGuid(),
            Name = request.Request.Name.Trim(),
            DiscountValue = request.Request.DiscountValue,
            StartDate = request.Request.StartDate,
            EndDate = request.Request.EndDate,
            IsActive = true
        };

        await promotionRepository.AddAsync(promotion, cancellationToken);

        var response = new CreatePromotionResponse(PromotionDtoMapper.ToDto(promotion));
        return Result<CreatePromotionResponse>.Success(response);
    }
}
