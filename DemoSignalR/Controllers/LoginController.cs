using Microsoft.AspNetCore.Mvc;
using DemoSignalR.Models.Data;
using DemoSignalR.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DemoSignalR.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["Error"] = "Username and password are required.";
                return View("Index");
            }

            // Find user by username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ViewData["Error"] = "Invalid username or password.";
                return View("Index");
            }

            // Hash the entered password and compare it with stored hash
            string hashedPassword = HashPassword(password);
            if (user.PasswordHash != hashedPassword) // If not matched
            {
                ViewData["Error"] = "Invalid username or password.";
                return View("Index");
            }

            // Store session and redirect
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Chat");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
