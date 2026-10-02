using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwaOlova.Portal.Controllers;
using SwaOlova.Application.Features.Promotions.Queries.GetPromotionById;
using SwaOlova.Application.Features.Promotions.Queries.GetCoupons;
using SwaOlova.Application.Features.Promotions.Commands.ActivatePromotion;
using SwaOlova.Application.Features.Promotions.Commands.DisablePromotion;

namespace SwaOlova.Portal.Areas.Promotions.Controllers
{
    [Area("Promotions")]
    public class PromotionsController : BaseController
    {
        public PromotionsController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            // This would use GetPromotionsPaged query when available
            return View();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var query = new GetPromotionByIdQuery(id);
            var result = await Mediator.Send(query);

            if (result == null)
                return NotFound();

            return View(result);
        }

        public async Task<IActionResult> Coupons(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetCouponsQuery();

            var result = await Mediator.Send(query);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(Guid id)
        {
            var command = new ActivatePromotionCommand(id);
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                TempData["Success"] = "Promotion activated successfully.";
            else
                TempData["Error"] = "Failed to activate promotion.";

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Disable(Guid id)
        {
            var command = new DisablePromotionCommand(id);
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                TempData["Success"] = "Promotion disabled successfully.";
            else
                TempData["Error"] = "Failed to disable promotion.";

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
