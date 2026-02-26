// HomeController.cs - Handles the home page of our website
// Every MVC app needs at least one controller

using CarInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarInsurance.Controllers
{
    // Controller class - handles HTTP requests and returns views
    public class HomeController : Controller
    {
        // Logger lets us write messages to the console for debugging
        private readonly ILogger<HomeController> _logger;

        // Constructor - the ILogger gets injected automatically by ASP.NET Core
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /  or  GET: /Home/Index
        // This is the main landing page
        public IActionResult Index()
        {
            // Just returns the Index.cshtml view from Views/Home/
            return View();
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // This action is called when an error occures
        // ResponseCache makes sure we dont cache error pages
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Pass the request ID to the view so we can show it in the error message
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
