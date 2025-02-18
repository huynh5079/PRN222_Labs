using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopeeFood4.Data;
using ShopeeFood4.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ShopeeFood4.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ShopeeFoodContext _context;

        public RestaurantController(ShopeeFoodContext context)
        {
            _context = context;
        }

        // GET: RestaurantController
        public async Task<IActionResult> GetAllRes()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        // GET: RestaurantController/Details/5
        public async Task<IActionResult> GetResById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: RestaurantController/Create
        public IActionResult CreateRes()
        {
            return View();
        }

        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRes([Bind("RestaurantId,Name,Description,Address,PhoneNumber,Email,OpenStatus")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllRes));
            }
            return View(restaurant);
        }

        // GET: RestaurantController/Edit/5
        public async Task<IActionResult> EditRes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRes(int id, [Bind("RestaurantId,Name,Description,Address,PhoneNumber,Email,OpenStatus")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetAllRes));
            }
            return View(restaurant);
        }

        // GET: RestaurantController/Delete/5
        public async Task<IActionResult> DeleteResById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: RestaurantController/Delete/5
        [HttpPost, ActionName("DeleteResById")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteResConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllRes));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.RestaurantId == id);
        }
    }
}