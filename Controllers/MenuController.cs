using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222_Lab5.Data;
using PRN222_Lab5.Extensions;
using PRN222_Lab5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN222_Lab5.Controllers
{
    public class MenuController : Controller
    {
        private readonly DBContext _context;

        public MenuController(DBContext context)
        {
            _context = context;
        }

        // Action to display the list of menu items
        public async Task<IActionResult> Index()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return View(menuItems);
        }

        // Action to display the details of a menu item
        public async Task<IActionResult> Details(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        // Action to add menu item to cart
        public IActionResult AddToCart(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            // Assuming Cart is stored in session
            List<MenuItem> cart;
            if (HttpContext.Session.GetObjectFromJson<List<MenuItem>>("Cart") == null)
            {
                cart = new List<MenuItem>();
            }
            else
            {
                cart = HttpContext.Session.GetObjectFromJson<List<MenuItem>>("Cart");
            }

            cart.Add(menuItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
    }
}
