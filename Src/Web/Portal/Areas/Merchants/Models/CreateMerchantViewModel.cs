using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SwaOlova.Portal.Areas.Merchants.Models;

public class CreateMerchantViewModel
{
    [Required(ErrorMessage = "Merchant Name is required")]
    [StringLength(200)]
    [Display(Name = "Merchant Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact Number is required")]
    [Phone]
    [Display(Name = "Contact Number")]
    public string ContactNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Merchant Category is required")]
    [Display(Name = "Merchant Category")]
    public Guid MerchantCategoryId { get; set; }

    public IReadOnlyCollection<SelectListItem> MerchantCategories { get; set; }
        = new List<SelectListItem>();

    [Required(ErrorMessage = "Address is required")]
    [Display(Name = "Address Line")]
    public string AddressLine1 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Village/Town is required")]
    [Display(Name = "Village/Town")]
    public string Village { get; set; } = string.Empty;

    [Range(-90, 90)]
    [Display(Name = "Latitude")]
    public decimal Latitude { get; set; }

    [Range(-180, 180)]
    [Display(Name = "Longitude")]
    public decimal Longitude { get; set; }
}