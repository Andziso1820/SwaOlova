using System.ComponentModel.DataAnnotations;

namespace SwaOlova.Portal.Areas.Merchants.Models
{
    public class UpdateMerchantViewModel
    {
        public Guid Id { get; set; }

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
}
