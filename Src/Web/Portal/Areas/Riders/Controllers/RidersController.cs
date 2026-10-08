using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwaOlova.Application.Features.Riders.Commands.AddRiderDocument;
using SwaOlova.Application.Features.Riders.Commands.ApproveRider;
using SwaOlova.Application.Features.Riders.Commands.CreateRider;
using SwaOlova.Application.Features.Riders.Commands.ReactivateRider;
using SwaOlova.Application.Features.Riders.Commands.SaveRiderVehicle;
using SwaOlova.Application.Features.Riders.Commands.SetAvailability;
using SwaOlova.Application.Features.Riders.Commands.SuspendRider;
using SwaOlova.Application.Features.Riders.Commands.UpdateLocation;
using SwaOlova.Application.Features.Riders.Commands.UpdateRider;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Application.Features.Riders.Queries.GetRiderActivities;
using SwaOlova.Application.Features.Riders.Queries.GetRiderById;
using SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;
using SwaOlova.Application.Features.Riders.Queries.GetRiderDocumentById;
using SwaOlova.Application.Features.Riders.Queries.SearchRiders;
using SwaOlova.Domain.Enums;
using SwaOlova.Portal.Areas.Riders.Models;
using SwaOlova.Portal.Controllers;
using SwaOlova.Portal.Models;

namespace SwaOlova.Portal.Areas.Riders.Controllers
{
    [Area("Riders")]
    public class RidersController : BaseController
    {
        private const long MaxDocumentSize = 10 * 1024 * 1024;

        public RidersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index(string view = "all", string? searchTerm = null)
        {
            var normalizedView = NormalizeView(view);
            var result = await Mediator.Send(new SearchRidersQuery(new SearchRidersRequest(searchTerm, null, 1, 100)));
                MapViewToStatus(normalizedView);

            var response = result.Value;
            var counts = response?.StatusCounts ?? new Dictionary<RiderStatus, int>();

            var model = new RiderIndexViewModel
            {
                ActiveView = normalizedView,
                SearchTerm = searchTerm?.Trim() ?? string.Empty,
                Heading = BuildHeading(normalizedView),
                Riders = response?.Results ?? [],
                DashboardCounts = BuildDashboardCounts(counts)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View(new CreateRiderViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRiderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await Mediator.Send(new CreateRiderCommand(new CreateRiderRequest(
                model.FirstName, model.LastName, model.PhoneNumber, model.Email, model.DriversLicenseNumber)));

            if (!result.Succeeded || result.Value is null)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create rider.");
                return View(model);
            }

            TempData["RiderActionMessage"] = "Rider created successfully. Capture the vehicle and documents before approval.";
            return RedirectToAction(nameof(Details), new { area = "Riders", id = result.Value.Rider.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await BuildDetailsAsync(id);
            if (model is null)
            {
                return NotFound();
            }

            model.RiderActionMessage = TempData["RiderActionMessage"]?.ToString();
            model.RiderErrorMessage = TempData["RiderErrorMessage"]?.ToString();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditForm(Guid id)
        {
            var rider = await GetRiderAsync(id);
            if (rider is null)
            {
                return NotFound();
            }

            return PartialView("_EditForm", new EditRiderViewModel
            {
                RiderId = rider.Id,
                FirstName = rider.FirstName,
                LastName = rider.LastName,
                PhoneNumber = rider.PhoneNumber,
                Email = rider.Email,
                DriversLicenseNumber = rider.DriversLicenseNumber
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditRiderViewModel model)
        {
            model.RiderId = id;
            if (!ModelState.IsValid)
            {
                return PartialView("_EditForm", model);
            }

            var result = await Mediator.Send(new UpdateRiderCommand(id, new UpdateRiderRequest(
                model.FirstName, model.LastName, model.PhoneNumber, model.Email, model.DriversLicenseNumber)));

            return ModalResult(result.Succeeded, result.Error, "_EditForm", model, id, "Rider details updated successfully.");
        }

        [HttpGet]
        public Task<IActionResult> ApproveForm(Guid id) => StatusFormAsync(id, "_ApproveForm");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id, RiderStatusViewModel model)
        {
            model.RiderId = id;
            var result = await Mediator.Send(new ApproveRiderCommand(id, model.Reason));
            return ModalResult(result.Succeeded, result.Error, "_ApproveForm", model, id, "Rider approved successfully.");
        }

        [HttpGet]
        public Task<IActionResult> SuspendForm(Guid id) => StatusFormAsync(id, "_SuspendForm");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Suspend(Guid id, RiderStatusViewModel model)
        {
            model.RiderId = id;
            if (string.IsNullOrWhiteSpace(model.Reason))
            {
                ModelState.AddModelError(nameof(model.Reason), "Suspension reason is required.");
                return PartialView("_SuspendForm", model);
            }

            var result = await Mediator.Send(new SuspendRiderCommand(id, model.Reason.Trim()));
            return ModalResult(result.Succeeded, result.Error, "_SuspendForm", model, id, "Rider suspended successfully.");
        }

        [HttpGet]
        public Task<IActionResult> ReactivateForm(Guid id) => StatusFormAsync(id, "_ReactivateForm");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(Guid id, RiderStatusViewModel model)
        {
            model.RiderId = id;
            if (string.IsNullOrWhiteSpace(model.Reason))
            {
                ModelState.AddModelError(nameof(model.Reason), "Reactivation reason is required.");
                return PartialView("_ReactivateForm", model);
            }

            var result = await Mediator.Send(new ReactivateRiderCommand(id, model.Reason.Trim()));
            return ModalResult(result.Succeeded, result.Error, "_ReactivateForm", model, id, "Rider reactivated successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> AvailabilityForm(Guid id)
        {
            var rider = await GetRiderAsync(id);
            return rider is null
                ? NotFound()
                : PartialView("_AvailabilityForm", new RiderAvailabilityViewModel
                {
                    RiderId = rider.Id,
                    RiderName = rider.FullName,
                    IsAvailable = rider.Status == RiderStatus.Available
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetAvailability(Guid id, RiderAvailabilityViewModel model)
        {
            model.RiderId = id;
            var result = await Mediator.Send(new SetAvailabilityCommand(id, new SetAvailabilityRequest(model.IsAvailable)));
            return ModalResult(result.Succeeded, result.Error, "_AvailabilityForm", model, id,
                $"Rider is now {(model.IsAvailable ? "available" : "offline")}.");
        }

        [HttpGet]
        public async Task<IActionResult> LocationForm(Guid id)
        {
            var rider = await GetRiderAsync(id);
            return rider is null
                ? NotFound()
                : PartialView("_LocationForm", new RiderLocationViewModel
                {
                    RiderId = rider.Id,
                    Latitude = rider.CurrentLocation?.Latitude,
                    Longitude = rider.CurrentLocation?.Longitude
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLocation(Guid id, RiderLocationViewModel model)
        {
            model.RiderId = id;
            if (!ModelState.IsValid)
            {
                return PartialView("_LocationForm", model);
            }

            var result = await Mediator.Send(new UpdateLocationCommand(id,
                new UpdateLocationRequest(model.Latitude!.Value, model.Longitude!.Value)));
            return ModalResult(result.Succeeded, result.Error, "_LocationForm", model, id, "Rider location updated successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> VehicleForm(Guid id)
        {
            var rider = await GetRiderAsync(id);
            if (rider is null)
            {
                return NotFound();
            }

            return PartialView("_VehicleForm", new RiderVehicleViewModel
            {
                RiderId = rider.Id,
                RegistrationNumber = rider.Vehicle?.RegistrationNumber ?? string.Empty,
                VehicleType = rider.Vehicle?.VehicleType ?? string.Empty,
                Make = rider.Vehicle?.Make ?? string.Empty,
                Model = rider.Vehicle?.Model ?? string.Empty
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveVehicle(Guid id, RiderVehicleViewModel vehicle)
        {
            vehicle.RiderId = id;
            if (!ModelState.IsValid)
            {
                return PartialView("_VehicleForm", vehicle);
            }

            var result = await Mediator.Send(new SaveRiderVehicleCommand(id,
                new SaveRiderVehicleRequest(vehicle.RegistrationNumber, vehicle.VehicleType, vehicle.Make, vehicle.Model)));
            return ModalResult(result.Succeeded, result.Error, "_VehicleForm", vehicle, id, "Rider vehicle saved successfully.");
        }

        [HttpGet]
        public IActionResult UploadDocumentForm(Guid id) =>
            PartialView("_UploadDocumentForm", new RiderDocumentUploadViewModel { RiderId = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(MaxDocumentSize + 1024 * 1024)]
        public async Task<IActionResult> UploadDocument(Guid id, RiderDocumentUploadViewModel model)
        {
            model.RiderId = id;

            if (model.DocumentFile is { Length: > MaxDocumentSize })
            {
                ModelState.AddModelError(nameof(model.DocumentFile), "Documents must be smaller than 10 MB.");
            }

            if (model.ExpiryDate.HasValue && model.ExpiryDate.Value.Date < DateTime.UtcNow.Date)
            {
                ModelState.AddModelError(nameof(model.ExpiryDate), "Expiry date cannot be in the past.");
            }

            if (!ModelState.IsValid || model.DocumentFile is null || model.DocumentType is null)
            {
                return PartialView("_UploadDocumentForm", model);
            }

            byte[] fileData;
            await using (var stream = new MemoryStream())
            {
                await model.DocumentFile.CopyToAsync(stream);
                fileData = stream.ToArray();
            }

            var result = await Mediator.Send(new AddRiderDocumentCommand(id, new AddRiderDocumentRequest(
                model.DocumentType.Value,
                model.DocumentFile.FileName,
                string.IsNullOrWhiteSpace(model.DocumentFile.ContentType) ? "application/octet-stream" : model.DocumentFile.ContentType,
                model.DocumentFile.Length,
                fileData,
                model.ExpiryDate)));

            return ModalResult(result.Succeeded, result.Error, "_UploadDocumentForm", model, id, "Rider document added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadDocument(Guid id)
        {
            var result = await Mediator.Send(new GetRiderDocumentByIdQuery(id));
            if (!result.Succeeded || result.Value is null)
            {
                return NotFound();
            }

            var document = result.Value;
            if (document.FileData is { Length: > 0 })
            {
                return File(document.FileData, document.ContentType ?? "application/octet-stream",
                    document.StoredFileName ?? document.FileName);
            }

            return Uri.TryCreate(document.FileUrl, UriKind.Absolute, out var uri) ? Redirect(uri.ToString()) : NotFound();
        }

        private async Task<IActionResult> StatusFormAsync(Guid id, string viewName)
        {
            var rider = await GetRiderAsync(id);
            return rider is null
                ? NotFound()
                : PartialView(viewName, new RiderStatusViewModel { RiderId = rider.Id, RiderName = rider.FullName });
        }

        private IActionResult ModalResult(bool succeeded, string? error, string viewName, object model, Guid id, string message)
        {
            if (!succeeded)
            {
                ModelState.AddModelError(string.Empty, error ?? "The operation could not be completed.");
                return PartialView(viewName, model);
            }

            return Json(new AjaxModalResult
            {
                Succeeded = true,
                Message = message,
                RedirectUrl = Url.Action(nameof(Details), new { area = "Riders", id })
            });
        }

        private async Task<RiderDto?> GetRiderAsync(Guid id)
        {
            var result = await Mediator.Send(new GetRiderByIdQuery(id));
            return result.Succeeded ? result.Value?.Rider : null;
        }

        private async Task<RiderDetailsViewModel?> BuildDetailsAsync(Guid id)
        {
            var rider = await GetRiderAsync(id);
            if (rider is null)
            {
                return null;
            }

            var deliveries = await Mediator.Send(new GetRiderDeliveriesQuery(id, 1, 10));
            var activities = await Mediator.Send(new GetRiderActivitiesQuery(id));

            return new RiderDetailsViewModel
            {
                Rider = rider,
                Deliveries = deliveries.Value?.Deliveries ?? [],
                TotalDeliveryCount = deliveries.Value?.TotalCount ?? 0,
                Activities = activities.Value ?? []
            };
        }

        private static IReadOnlyDictionary<string, int> BuildDashboardCounts(IReadOnlyDictionary<RiderStatus, int> counts)
        {
            int Count(RiderStatus status) => counts.TryGetValue(status, out var value) ? value : 0;

            return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["all"] = counts.Values.Sum(),
                ["pending"] = Count(RiderStatus.PendingApproval),
                ["available"] = Count(RiderStatus.Available),
                ["busy"] = Count(RiderStatus.Busy),
                ["offline"] = Count(RiderStatus.Offline),
                ["suspended"] = Count(RiderStatus.Suspended)
            };
        }

        private static RiderStatus? MapViewToStatus(string view) => view switch
        {
            "pending" => RiderStatus.PendingApproval,
            "available" => RiderStatus.Available,
            "busy" => RiderStatus.Busy,
            "offline" => RiderStatus.Offline,
            "suspended" => RiderStatus.Suspended,
            _ => null
        };

        private static string NormalizeView(string? view) =>
            string.IsNullOrWhiteSpace(view) ? "all" : view.Trim().ToLowerInvariant();

        private static string BuildHeading(string view) => view switch
        {
            "pending" => "Riders Pending Approval",
            "available" => "Available Riders",
            "busy" => "Busy Riders",
            "offline" => "Offline Riders",
            "suspended" => "Suspended Riders",
            _ => "Manage Riders"
        };
    }
}
