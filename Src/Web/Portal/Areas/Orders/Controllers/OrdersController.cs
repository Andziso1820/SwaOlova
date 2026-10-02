using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwaOlova.Portal.Controllers;
using SwaOlova.Application.Features.Orders.Queries.GetOrderById;
using SwaOlova.Application.Features.Orders.Queries.GetOrdersPaged;
using SwaOlova.Application.Features.Orders.Queries.SearchOrders;

namespace SwaOlova.Portal.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetOrdersPagedQuery(new GetOrdersPagedRequest(pageNumber, pageSize));

            var result = await Mediator.Send(query);
            return View(result);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await Mediator.Send(query);

            if (result == null)
                return NotFound();

            return View(result);
        }

        public async Task<IActionResult> Search(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var query = new SearchOrdersQuery(new SearchOrdersRequest(searchTerm, null, null, pageNumber, pageSize));

            var result = await Mediator.Send(query);
            return View("Index", result);
        }
    }
}
