using System.ComponentModel.DataAnnotations;

namespace SwaOlova.Portal.Models;

/// <summary>
/// ViewModel for user login form
/// </summary>
public class LoginViewModel
{
    [Required(ErrorMessage = "Email or Username is required")]
    [Display(Name = "Email or Username")]
    public string EmailOrUsername { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
