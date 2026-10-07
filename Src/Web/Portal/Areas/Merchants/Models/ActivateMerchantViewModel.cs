namespace SwaOlova.Portal.Areas.Merchants.Models;

public sealed class ActivateMerchantViewModel
{
    public Guid Id { get; set; }

    public string MerchantName { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;
}