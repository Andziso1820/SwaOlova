namespace SwaOlova.Application.Features.Merchants.Dtos;

public sealed record MerchantActivityDto(
    DateTime Date,
    string Title,
    string Description);
