using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;
using SwaOlova.Domain.Enums;
using DomainRider = SwaOlova.Domain.Rider.Rider;

namespace SwaOlova.Portal.Areas.Riders.Models;

public class RiderFormViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    [StringLength(20)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(256)]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Driver's license number is required")]
    [StringLength(100)]
    [Display(Name = "Driver's License Number")]
    public string DriversLicenseNumber { get; set; } = string.Empty;
}

public sealed class CreateRiderViewModel : RiderFormViewModel
{
}

public sealed class EditRiderViewModel : RiderFormViewModel
{
    public Guid RiderId { get; set; }
}

public sealed class RiderStatusViewModel
{
    public Guid RiderId { get; set; }

    public string RiderName { get; set; } = string.Empty;

    [StringLength(1000)]
    [Display(Name = "Reason / Notes")]
    public string? Reason { get; set; }
}

public sealed class RiderAvailabilityViewModel
{
    public Guid RiderId { get; set; }

    public string RiderName { get; set; } = string.Empty;

    [Display(Name = "Available for deliveries")]
    public bool IsAvailable { get; set; }
}

public sealed class RiderLocationViewModel
{
    public Guid RiderId { get; set; }

    [Required]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
    public decimal? Latitude { get; set; }

    [Required]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
    public decimal? Longitude { get; set; }
}

public sealed class RiderVehicleViewModel
{
    public Guid RiderId { get; set; }

    [Required(ErrorMessage = "Registration number is required")]
    [StringLength(50)]
    [Display(Name = "Registration Number")]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vehicle type is required")]
    [StringLength(100)]
    [Display(Name = "Vehicle Type")]
    public string VehicleType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Make is required")]
    [StringLength(100)]
    public string Make { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model is required")]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    public static IReadOnlyCollection<string> VehicleTypes { get; } = ["Motorcycle", "Bicycle", "Scooter", "Car", "Bakkie", "Van"];
}

public sealed class RiderDocumentUploadViewModel
{
    public Guid RiderId { get; set; }

    [Required(ErrorMessage = "Document type is required")]
    [Display(Name = "Document Type")]
    public RiderDocumentType? DocumentType { get; set; }

    [Required(ErrorMessage = "Please select a file")]
    [Display(Name = "Document File")]
    public IFormFile? DocumentFile { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Expiry Date")]
    public DateTime? ExpiryDate { get; set; }
}

public sealed class RiderDetailsViewModel
{
    public required RiderDto Rider { get; init; }

    public IReadOnlyCollection<DeliverySummaryDto> Deliveries { get; init; } = [];

    public int TotalDeliveryCount { get; init; }

    public IReadOnlyCollection<RiderActivityDto> Activities { get; init; } = [];

    public string? RiderActionMessage { get; set; }

    public string? RiderErrorMessage { get; set; }

    public int CompletedDeliveries => Deliveries.Count(d => string.Equals(d.Status, "Completed", StringComparison.OrdinalIgnoreCase)
        || string.Equals(d.Status, "Delivered", StringComparison.OrdinalIgnoreCase));

    public decimal SuccessRate => Deliveries.Count == 0
        ? 0
        : Math.Round((decimal)CompletedDeliveries / Deliveries.Count * 100, 1);

    public bool HasVehicle => Rider.Vehicle is not null;

    public bool CanApprove => DomainRider.CanApprove(Rider.Status);

    public bool CanSuspend => DomainRider.CanSuspend(Rider.Status);

    public bool CanReactivate => DomainRider.CanReactivate(Rider.Status);

    public bool CanChangeAvailability => DomainRider.CanChangeAvailability(Rider.Status);

    public bool CanUpdateLocation => DomainRider.CanTrackLocation(Rider.Status);

    public bool CanEditRider => Rider.Status != RiderStatus.None;

    public bool CanApproveRider => CanApprove && HasVehicle;

    public bool CanManageDocuments => Rider.Status != RiderStatus.None;

    public bool CanManageVehicle => Rider.Status != RiderStatus.None;

    public bool HasOutstandingDocuments => Rider.Documents.Count == 0 || Rider.Documents.Any(d => d.IsExpired);

    public string LocationSummary => Rider.CurrentLocation is null
        ? "No live location recorded."
        : $"{Rider.CurrentLocation.Latitude:F6}, {Rider.CurrentLocation.Longitude:F6} · {Rider.CurrentLocation.RecordedAt:dd MMM yyyy HH:mm}";

    public static string BadgeClass(RiderStatus status) => RiderIndexViewModel.BadgeClass(status.ToString());
}
