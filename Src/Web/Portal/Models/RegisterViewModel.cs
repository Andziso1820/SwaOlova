using System.ComponentModel.DataAnnotations;

namespace SwaOlova.Portal.Models;

/// <summary>
/// ViewModel for user registration form
/// </summary>
public class RegisterViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    [Display(Name = "First Name")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    [Display(Name = "Last Name")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email")]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character (@$!%*?&)")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Phone Number")]
    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "I agree to the Terms of Service")]
    public bool AgreedToTerms { get; set; }
}
