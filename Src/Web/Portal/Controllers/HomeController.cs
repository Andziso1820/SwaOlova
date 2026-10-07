using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SwaOlova.Portal.Models;

namespace SwaOlova.Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? requestId = null)
        {
            var model = new ErrorViewModel
            {
                RequestId = requestId ?? Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return View(model);
        }
    }
}
