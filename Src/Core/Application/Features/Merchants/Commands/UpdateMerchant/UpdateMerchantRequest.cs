namespace SwaOlova.Application.Features.Merchants.Commands.UpdateMerchant;

public sealed record UpdateMerchantRequest(
    string Name,
    string ContactNumber,
    string AddressLine1,
    string Village,
    decimal Latitude,
    decimal Longitude);
