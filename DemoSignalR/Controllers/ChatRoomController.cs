using DemoSignalR.Models.Data;
using DemoSignalR.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoSignalR.Controllers
{
    public class ChatRoomController : Controller
    {
        private readonly DatabaseContext _context;

        public ChatRoomController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult ChatRoom(Guid roomId)
        {
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Login");
            }

            var messages = _context.Messages
                .Where(m => m.RoomId == roomId)
                .OrderBy(m => m.SentAt)
                .ToList();

            return View(messages);
        }
    }
}
