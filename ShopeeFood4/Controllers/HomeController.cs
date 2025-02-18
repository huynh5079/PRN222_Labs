using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood4.Models;
using ShopeeFood4.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ShopeeFood4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopeeFoodContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ListRes()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        public IActionResult CreateRes()
        {
            return View();
        }

        public IActionResult EditRes()
        {
            return View();
        }

        public IActionResult GetResById()
        {
            return View();
        }

        public IActionResult DeleteRes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
