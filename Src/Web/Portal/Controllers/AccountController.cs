using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwaOlova.Application.Common.Interfaces.Services;
using SwaOlova.Infrastructure.Data.Identity;
using SwaOlova.Portal.Models;
using System.Security.Claims;

namespace SwaOlova.Portal.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private const long MaxProfilePhotoSize = 5 * 1024 * 1024;
    private static readonly HashSet<string> AllowedProfilePhotoContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/webp"
    };

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IFileStorageService fileStorageService,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Try to find user by email or username
        var user = await _userManager.FindByEmailAsync(model.EmailOrUsername)
            ?? await _userManager.FindByNameAsync(model.EmailOrUsername);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt. User not found.");
            _logger.LogWarning($"Login attempt with unknown email/username: {model.EmailOrUsername}");
            return View(model);
        }

        // Check if user is active
        if (!user.IsActive)
        {
            ModelState.AddModelError(string.Empty, "Your account has been deactivated. Please contact support.");
            _logger.LogWarning($"Login attempt with deactivated account: {user.Email}");
            return View(model);
        }

        // Check if user is locked
        if (user.IsLocked)
        {
            ModelState.AddModelError(string.Empty, "Your account has been locked. Please contact support.");
            _logger.LogWarning($"Login attempt with locked account: {user.Email}");
            return View(model);
        }

        // Try to sign in with password
        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            // Update last login date
            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            _logger.LogInformation($"User {user.Email} logged in successfully");

            // Redirect to return URL if provided, otherwise to Home
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning($"User account locked out: {user.Email}");
            ModelState.AddModelError(string.Empty, "User account locked out for 5 minutes due to multiple failed login attempts.");
            return View(model);
        }

        if (result.RequiresTwoFactor)
        {
            return RedirectToAction("LoginWith2Fa", new { returnUrl });
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
        _logger.LogWarning($"Failed login attempt for user: {user.Email}");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError(nameof(model.Email), "User with this email already exists.");
            return View(model);
        }

        // Create new user
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            EmailConfirmed = false,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            _logger.LogWarning($"Failed to register user: {model.Email}");
            return View(model);
        }

        // Assign default role to new user
        var roleAssignmentResult = await _userManager.AddToRoleAsync(user, "Customer");
        if (!roleAssignmentResult.Succeeded)
        {
            _logger.LogWarning($"Failed to assign Customer role to new user: {model.Email}");
            // Continue anyway, user is created but role assignment failed
        }

        _logger.LogInformation($"New user registered: {model.Email}");

        // Show success message and redirect to login
        TempData["SuccessMessage"] = "Registration successful. Please log in with your credentials.";
        return RedirectToAction("Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation($"User {User.FindFirst(ClaimTypes.Email)?.Value} logged out");

        TempData["SuccessMessage"] = "You have been successfully logged out.";
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    [RequestSizeLimit(MaxProfilePhotoSize + 1024 * 1024)]
    public async Task<IActionResult> UploadProfilePhoto(IFormFile? profilePhoto)
    {
        if (profilePhoto is null || profilePhoto.Length == 0)
        {
            TempData["ErrorMessage"] = "Please select an image to upload.";
            return RedirectToAction(nameof(Profile));
        }

        if (profilePhoto.Length > MaxProfilePhotoSize)
        {
            TempData["ErrorMessage"] = "Profile photos must be 5 MB or smaller.";
            return RedirectToAction(nameof(Profile));
        }

        if (!IsSupportedProfilePhoto(profilePhoto))
        {
            TempData["ErrorMessage"] = "Only JPG, PNG, GIF, and WEBP images are allowed.";
            return RedirectToAction(nameof(Profile));
        }

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var previousProfilePhotoPath = user.ProfilePhotoUrl;

        try
        {
            await using var stream = profilePhoto.OpenReadStream();
            var uploadedPhotoPath = await _fileStorageService.UploadAsync(
                "profile",
                profilePhoto.FileName,
                stream,
                profilePhoto.ContentType);

            user.ProfilePhotoUrl = uploadedPhotoPath;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                await _fileStorageService.DeleteAsync(uploadedPhotoPath);
                TempData["ErrorMessage"] = result.Errors.FirstOrDefault()?.Description ?? "Unable to update your profile photo.";
                return RedirectToAction(nameof(Profile));
            }

            if (!string.IsNullOrWhiteSpace(previousProfilePhotoPath)
                && !string.Equals(previousProfilePhotoPath, uploadedPhotoPath, StringComparison.OrdinalIgnoreCase))
            {
                await _fileStorageService.DeleteAsync(previousProfilePhotoPath);
            }

            _logger.LogInformation("User {Email} updated profile photo successfully", user.Email);
            TempData["SuccessMessage"] = "Your profile photo has been updated successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload profile photo for user {Email}", user.Email);
            TempData["ErrorMessage"] = "Unable to upload your profile photo right now.";
        }

        return RedirectToAction(nameof(Profile));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
    {
        if (newPassword != confirmPassword)
        {
            ModelState.AddModelError(string.Empty, "New password and confirmation do not match.");
            return RedirectToAction("Profile");
        }

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Profile");
        }

        _logger.LogInformation($"User {user.Email} changed password successfully");
        TempData["SuccessMessage"] = "Your password has been changed successfully.";
        return RedirectToAction("Profile");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(string firstName, string lastName, string phoneNumber)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = firstName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Profile");
        }

        _logger.LogInformation($"User {user.Email} updated profile");
        TempData["SuccessMessage"] = "Your profile has been updated successfully.";
        return RedirectToAction("Profile");
    }

    private static bool IsSupportedProfilePhoto(IFormFile profilePhoto)
    {
        if (AllowedProfilePhotoContentTypes.Contains(profilePhoto.ContentType))
        {
            return true;
        }

        var extension = Path.GetExtension(profilePhoto.FileName);
        return extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".gif", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".webp", StringComparison.OrdinalIgnoreCase);
    }
}
