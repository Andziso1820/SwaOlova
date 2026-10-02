namespace SwaOlova.Application.Features.Merchants.Commands.CreateMerchant;

public sealed record CreateMerchantRequest(
    string Name,
    string ContactNumber,
    Guid MerchantCategoryId,
    string AddressLine1,
    string Village,
    decimal Latitude,
    decimal Longitude);
