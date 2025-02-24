using Microsoft.AspNetCore.Mvc;
using PRN222_Lab5.Extensions;
using PRN222_Lab5.Models;
using PRN222_Lab5.Data;
using System.Collections.Generic;

namespace PRN222_Lab5.Controllers
{
    public class CartController : Controller
    {
        private readonly DBContext _context;

        public CartController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<MenuItem>>("Cart") ?? new List<MenuItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem == null)
            {
                return NotFound();
            }

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

            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<MenuItem>>("Cart") ?? new List<MenuItem>();
            var menuItem = cart.Find(item => item.MenuItemId == id);
            if (menuItem != null)
            {
                cart.Remove(menuItem);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }
    }
}
