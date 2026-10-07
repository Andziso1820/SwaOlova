using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Commands.ActivateMerchant;
using SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;
using SwaOlova.Application.Features.Merchants.Commands.ApproveMerchant;
using SwaOlova.Application.Features.Merchants.Commands.CloseMerchant;
using SwaOlova.Application.Features.Merchants.Commands.CreateMerchant;
using SwaOlova.Application.Features.Merchants.Commands.SuspendMerchant;
using SwaOlova.Application.Features.Merchants.Commands.UpdateMerchant;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Application.Features.Merchants.Queries.GetMerchantActivities;
using SwaOlova.Application.Features.Merchants.Queries.GetMerchantById;
using SwaOlova.Application.Features.Merchants.Queries.GetMerchantCategories;
using SwaOlova.Application.Features.Merchants.Queries.GetMerchantComplianceDocumentById;
using SwaOlova.Application.Features.Merchants.Queries.GetMerchantsPaged;
using SwaOlova.Domain.Enums;
using SwaOlova.Portal.Areas.Merchants.Models;
using SwaOlova.Portal.Controllers;
using SwaOlova.Portal.Models;

namespace SwaOlova.Portal.Areas.Merchants.Controllers
{
    [Area("Merchants")]
    public class MerchantsController : BaseController
    {
        public MerchantsController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> Index(
             string view = "dashboard",
             int pageNumber = 1,
             int pageSize = 100)
        {
            var result = await Mediator.Send(
                new GetMerchantsPagedQuery(
                    pageNumber,
                    pageSize));

            if (!result.IsSuccess)
            {
                return View(new MerchantIndexViewModel());
            }

            var merchants = result.Value.Items.ToList();

            var model = new MerchantIndexViewModel
            {
                ActiveView = view,
                Merchants = merchants,

                Heading = view switch
                {
                    "pending" => "Pending Approval Merchants",
                    "active" => "Active Merchants",
                    "suspended" => "Suspended Merchants",
                    _ => "Merchant Dashboard"
                },

                DashboardCounts = new Dictionary<string, int>
                {
                    ["total"] =
                        merchants.Count,

                    ["pending"] =
                        merchants.Count(x =>
                            x.Status == MerchantStatus.PendingApproval),

                    ["active"] =
                        merchants.Count(x =>
                            x.Status == MerchantStatus.Active),

                    ["suspended"] =
                        merchants.Count(x =>
                            x.Status == MerchantStatus.Suspended),

                    ["restaurants"] =
                        merchants.Count,

                    ["pharmacies"] =
                        0
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateMerchantViewModel();

            await PopulateMerchantCategoriesAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMerchantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value!.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                await PopulateMerchantCategoriesAsync(model);
                return View(model);
            }

            var command = new CreateMerchantCommand(
                new CreateMerchantRequest(
                    model.Name,
                    model.ContactNumber,
                    model.MerchantCategoryId,
                    model.AddressLine1,
                    model.Village,
                    model.Latitude,
                    model.Longitude));

            var result = await Mediator.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to create merchant.");

                await PopulateMerchantCategoriesAsync(model);
                return View(model);
            }

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = result.Value.Merchant.Id
                });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await BuildMerchantWorkspaceViewModelAsync(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UploadDocumentForm(Guid id)
        {
            var merchantResult = await Mediator.Send(new GetMerchantByIdQuery(id));
            if (!merchantResult.IsSuccess)
            {
                return NotFound();
            }

            var model = new UploadViewModel
            {
                MerchantId = id
            };
            return PartialView("_UploadDocumentForm", model);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadDocument(Guid documentId)
        {
            var result = await Mediator.Send(new GetMerchantComplianceDocumentByIdQuery(documentId));
            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var document = result.Value;
            if (document.FileData is null || document.FileData.Length == 0)
            {
                return string.IsNullOrWhiteSpace(document.FileUrl)
                    ? NotFound()
                    : Redirect(document.FileUrl);
            }

            var contentType = string.IsNullOrWhiteSpace(document.ContentType)
                ? "application/octet-stream"
                : document.ContentType;

            var fileName = string.IsNullOrWhiteSpace(document.StoredFileName)
                ? document.Name
                : document.StoredFileName;

            return File(document.FileData, contentType, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadDocument(Guid id, UploadViewModel model)
        {
            model.MerchantId = id;

            if (!ModelState.IsValid)
            {
                return PartialView("_UploadDocumentForm", model);
            }

            await using var memoryStream = new MemoryStream();
            await model.DocumentFile!.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var result = await Mediator.Send(
                new AddComplianceDocumentCommand(
                    id,
                    new AddComplianceDocumentRequest(
                        model.DocumentType,
                        model.DocumentFile.FileName,
                        string.IsNullOrWhiteSpace(model.DocumentFile.ContentType) ? "application/octet-stream" : model.DocumentFile.ContentType,
                        model.DocumentFile.Length,
                        fileBytes,
                        model.DocumentExpiryDate)));

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to add compliance document.");
                return PartialView("_UploadDocumentForm", model);
            }

            return Json(new AjaxModalResult
            {
                Succeeded = true,
                Message = "Compliance document added successfully.",
                RedirectUrl = Url.Action(nameof(Details), new { area = "Merchants", id })
            });
        }

        [HttpGet]
        public async Task<IActionResult> EditForm(Guid id)
        {
            var result =
                await Mediator.Send(
                    new GetMerchantByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var merchant = result.Value;

            var model = new UpdateMerchantViewModel
            {
                Id = merchant.Id,
                Name = merchant.Name,
                ContactNumber = merchant.ContactNumber,
                MerchantCategoryId = merchant.MerchantCategoryId,
                AddressLine1 = merchant.Address.AddressLine1,
                Village = merchant.Address.Village,
                Latitude = merchant.Address.Latitude,
                Longitude = merchant.Address.Longitude
            };

            return PartialView("_EditForm", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditForm(UpdateMerchantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_EditForm", model);
            }

            var command =
                new UpdateMerchantCommand(
                    model.Id,
                    new UpdateMerchantRequest(
                        model.Name,
                        model.ContactNumber,
                        model.AddressLine1,
                        model.Village,
                        model.Latitude,
                        model.Longitude));

            var result =
                await Mediator.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.Error);

                return View("_EditForm", model);
            }

            return Json(new
            {
                Succeeded = true,
                Message = "Merchant details updated.",
                RedirectUrl = Url.Action(
                        "Details",
                        "Merchants",
                        new
                        {
                            area = "Merchants",
                            id = model.Id
                        })
            });
        }

        [HttpGet]
        public async Task<IActionResult> ApproveForm(Guid id)
        {
            var result =
                await Mediator.Send(
                    new GetMerchantByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var merchant = result.Value;

            var model = new ApproveMerchantViewModel
            {
                Id = merchant.Id,
                MerchantName = merchant.Name
            };

            return PartialView("_ApproveForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveForm(ApproveMerchantViewModel model)
        {
            var command =
                new ApproveMerchantCommand(model.Id);

            try
            {
                var result =
               await Mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return Json(new
                    {
                        Succeeded = false,
                        Message = result.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Succeeded = false,
                    Message = ex.ToString()
                });
            }
           

            return Json(new
            {
                Succeeded = true,
                Message = "Merchant approved successfully.",
                RedirectUrl = Url.Action(
                    "Details",
                    "Merchants",
                    new
                    {
                        area = "Merchants",
                        id = model.Id
                    })
            });
        }

        [HttpGet]
        public async Task<IActionResult> SuspendForm(Guid id)
        {
            var result =
                await Mediator.Send(
                    new GetMerchantByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var model = new SuspendMerchantViewModel
            {
                Id = result.Value.Id,
                MerchantName = result.Value.Name
            };

            return PartialView("_SuspendForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuspendForm(SuspendMerchantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_SuspendForm",
                    model);
            }

            var command =
                new SuspendMerchantCommand(
                    model.Id,
                    model.Reason);

            var result =
                await Mediator.Send(command);

            if (!result.IsSuccess)
            {
                return Json(new
                {
                    Succeeded = false,
                    Message = result.Error
                });
            }

            return Json(new
            {
                Succeeded = true,
                Message = "Merchant suspended successfully.",
                RedirectUrl = Url.Action(
                    "Details",
                    "Merchants",
                    new
                    {
                        area = "Merchants",
                        id = model.Id
                    })
            });
        }
        [HttpGet]
        public async Task<IActionResult> ActivateForm(Guid id)
        {
            var result =
                await Mediator.Send(
                    new GetMerchantByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var model =
                new ActivateMerchantViewModel
                {
                    Id = result.Value.Id,
                    MerchantName = result.Value.Name
                };

            return PartialView(
                "_ActivateForm",
                model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateForm(ActivateMerchantViewModel model)
        {
            var command =
                new ActivateMerchantCommand(
                    model.Id,
                    model.Reason);

            var result =
                await Mediator.Send(command);

            if (!result.IsSuccess)
            {
                return Json(new
                {
                    Succeeded = false,
                    Message = result.Error
                });
            }

            return Json(new
            {
                Succeeded = true,
                Message = "Merchant activated successfully.",
                RedirectUrl = Url.Action(
                    "Details",
                    "Merchants",
                    new
                    {
                        area = "Merchants",
                        id = model.Id
                    })
            });
        }

        [HttpGet]
        public async Task<IActionResult> CloseMerchantForm(Guid id)
        {
            var result =
                await Mediator.Send(
                    new GetMerchantByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var model = new CloseMerchantViewModel
            {
                Id = result.Value.Id,
                MerchantName = result.Value.Name
            };

            return PartialView(
                "_CloseMerchantForm",
                model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseMerchantForm(
    CloseMerchantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_CloseMerchantForm",
                    model);
            }

            var command =
                new CloseMerchantCommand(
                    model.Id,
                    model.Reason);

            var result =
                await Mediator.Send(command);

            if (!result.IsSuccess)
            {
                return Json(new
                {
                    Succeeded = false,
                    Message = result.Error
                });
            }

            return Json(new
            {
                Succeeded = true,
                Message = "Merchant closed successfully.",
                RedirectUrl = Url.Action(
                    "Details",
                    "Merchants",
                    new
                    {
                        area = "Merchants",
                        id = model.Id
                    })
            });
        }

        private async Task<MerchantWorkspaceViewModel?> BuildMerchantWorkspaceViewModelAsync(Guid id, MerchantWorkspaceViewModel? postedValues = null)
        {
            var result = await Mediator.Send(new GetMerchantByIdQuery(id));
            if (!result.IsSuccess)
            {
                return null;
            }

            var merchant = result.Value;
            var documents = merchant.ComplianceDocuments
                .Select(document => new DocumentVm
                {
                    Id = document.Id,
                    Name = document.Name,
                    FileUrl = document.FileUrl,
                    ExpiryDate = document.ExpiryDate,
                    UploadedAt = document.CreatedDate,
                    Status = GetDocumentStatus(document)
                })
                .OrderByDescending(document => document.UploadedAt)
                .ToList();

            var activityResult = await Mediator.Send(new GetMerchantActivitiesQuery(id));
            var activities = activityResult.IsSuccess
                ? activityResult.Value
                    .Select(activity => new ActivityVm
                    {
                        Date = activity.Date,
                        Title = activity.Title,
                        Description = activity.Description
                    })
                    .ToList()
                : new List<ActivityVm>();

            return new MerchantWorkspaceViewModel
            {
                Merchant = merchant,
                CanEditMerchant = merchant.Status != MerchantStatus.Closed,
                CanApproveMerchant = merchant.Status == MerchantStatus.PendingApproval,
                CanSuspendMerchant = merchant.Status == MerchantStatus.Active,
                CanActivateMerchant = merchant.Status == MerchantStatus.Suspended,
                CanCloseMerchant = merchant.Status == MerchantStatus.Active || merchant.Status == MerchantStatus.Suspended,
                CanManageDocuments = merchant.Status != MerchantStatus.Closed,
                CanManageUsers = true,
                CanViewOrders = true,
                CanViewProducts = true,
                CanViewFinancials = true,
                ProductsCount = merchant.Products.Count,
                ActiveProducts = merchant.Products.Count(product => string.Equals(product.Status, "Active", StringComparison.OrdinalIgnoreCase)),
                OrdersToday = 0,
                OrdersThisMonth = 0,
                RevenueThisMonth = 0,
                MerchantUsers = 0,
                AverageRating = 0,
                ReviewsCount = 0,
                HasOutstandingDocuments = !documents.Any() || documents.Any(document => string.Equals(document.Status, "Expired", StringComparison.OrdinalIgnoreCase)),
                DocumentFileUrl = postedValues?.DocumentFileUrl ?? string.Empty,
                DocumentExpiryDate = postedValues?.DocumentExpiryDate,
                DocumentType = postedValues?.DocumentType ?? 0,
                Documents = documents,
                RecentActivities = activities
                    .OrderByDescending(activity => activity.Date)
                    .ToList()
            };
        }

        private async Task PopulateMerchantCategoriesAsync(CreateMerchantViewModel model)
        {
            var result = await Mediator.Send(new GetMerchantCategoriesQuery());

            model.MerchantCategories = result.IsSuccess
                ? result.Value
                    .Select(category => new SelectListItem(
                        category.Name,
                        category.Id.ToString(),
                        category.Id == model.MerchantCategoryId))
                    .ToList()
                : new List<SelectListItem>();
        }

        private static string GetDocumentStatus(MerchantComplianceDocumentDto document)
        {
            if (document.ExpiryDate.HasValue && document.ExpiryDate.Value.Date < DateTime.UtcNow.Date)
            {
                return "Expired";
            }

            return "Active";
        }
    }
}
