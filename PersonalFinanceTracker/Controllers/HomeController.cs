using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Helpers;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JwtHelper jwtHelper;

        public HomeController(ILogger<HomeController> logger, JwtHelper jwtHelper):base(jwtHelper)
        {
            _logger = logger;
            this.jwtHelper = jwtHelper;
        }

        public IActionResult Index()
        {
            if (!JwtHelper.IsUserLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "account");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            if (!JwtHelper.IsUserLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "account");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
