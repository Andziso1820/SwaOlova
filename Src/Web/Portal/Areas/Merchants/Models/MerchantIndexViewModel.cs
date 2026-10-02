using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Portal.Areas.Merchants.Models;

public class MerchantIndexViewModel
{
    public string ActiveView { get; set; } = "dashboard";

    public string Heading { get; set; } = "Merchants";

    public IReadOnlyCollection<MerchantDto> Merchants { get; set; }
        = new List<MerchantDto>();

    public Dictionary<string, int> DashboardCounts { get; set; }
        = new();

    public int TotalCount => Merchants.Count;

    public IReadOnlyCollection<MerchantDto> FilteredMerchants
    {
        get
        {
            return ActiveView?.ToLowerInvariant() switch
            {
                "dashboard" => Merchants,
                "active" or "approved" => Merchants.Where(m => m.Status == Domain.Enums.MerchantStatus.Active).ToList(),
                "suspended" => Merchants.Where(m => m.Status == Domain.Enums.MerchantStatus.Suspended).ToList(),
                "pending" => Merchants.Where(m => m.Status == Domain.Enums.MerchantStatus.PendingApproval).ToList(),
                "closed" => Merchants.Where(m => m.Status == Domain.Enums.MerchantStatus.Closed).ToList(),
                _ => Merchants
            };
        }
    }
}
