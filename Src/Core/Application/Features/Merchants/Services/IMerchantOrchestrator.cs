using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Commands.CreateMerchant;
using SwaOlova.Application.Features.Merchants.Commands.UpdateMerchant;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Services;

public interface IMerchantOrchestrator
{
    Task<Result<MerchantDto>> CreateMerchantAsync(
        CreateMerchantRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<MerchantDto>> UpdateMerchantAsync(
        Guid merchantId,
        UpdateMerchantRequest request,
        CancellationToken cancellationToken = default);

    Task<Result> ApproveMerchantAsync(
        Guid merchantId,
        CancellationToken cancellationToken = default);

    Task<Result> ActivateMerchantAsync(
        Guid merchantId,
        CancellationToken cancellationToken = default);

    Task<Result> SuspendMerchantAsync(
        Guid merchantId,
        CancellationToken cancellationToken = default);

    Task<Result> UpdateBusinessHoursAsync(
        Guid merchantId,
        IReadOnlyCollection<(string day, TimeOnly opening, TimeOnly closing, bool isClosed)> hours,
        CancellationToken cancellationToken = default);

    Task<Result<MerchantDto>> GetMerchantAsync(
        Guid merchantId,
        CancellationToken cancellationToken = default);

    Task<Result<PagedResult<MerchantDto>>> GetMerchantsPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}
