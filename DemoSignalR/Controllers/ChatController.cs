using DemoSignalR.Models.Data;
using DemoSignalR.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace DemoSignalR.Controllers
{
    public class ChatController : Controller
    {
        private readonly DatabaseContext _context;

        public ChatController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Login");
            }

            // Fetch all users except the current user
            var users = _context.Users
                .Where(u => u.UserId.ToString() != currentUserId)
                .Where(u => string.IsNullOrEmpty(search) || u.Username.Contains(search))
                .ToList();

            return View(users);
        }

        [HttpPost]
        public IActionResult CreateOrJoinRoom(Guid userId)
        {
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Login");
            }

            // Check if chat room exists between these users
            var chatRoom = _context.ChatRooms
                .FirstOrDefault(cr => cr.UserRooms.Any(ur => ur.UserId.ToString() == currentUserId) &&
                                      cr.UserRooms.Any(ur => ur.UserId == userId));

            if (chatRoom == null)
            {
                // Create a new chat room
                chatRoom = new ChatRoom { RoomId = Guid.NewGuid() };
                _context.ChatRooms.Add(chatRoom);
                _context.SaveChanges();

                // Add users to the room
                _context.UserRooms.Add(new UserRoom { RoomId = chatRoom.RoomId, UserId = Guid.Parse(currentUserId) });
                _context.UserRooms.Add(new UserRoom { RoomId = chatRoom.RoomId, UserId = userId });
                _context.SaveChanges();
            }

            return RedirectToAction("ChatRoom", "ChatRoom", new { roomId = chatRoom.RoomId });
        }
    }
}
