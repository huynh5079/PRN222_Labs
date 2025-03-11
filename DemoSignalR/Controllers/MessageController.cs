using DemoSignalR.Hubs;
using DemoSignalR.Models.Data;
using DemoSignalR.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace DemoSignalR.Controllers
{
    public class MessageController : Controller
    {
        private readonly DatabaseContext _context;

        public MessageController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult SendMessage(Guid roomId, string message)
        {
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized("User not logged in.");
            }

            var chatRoom = _context.ChatRooms.FirstOrDefault(r => r.RoomId == roomId);
            if (chatRoom == null)
            {
                return BadRequest("Chat room does not exist.");
            }

            var userId = Guid.Parse(currentUserId);
            var isUserInRoom = _context.UserRooms.Any(ur => ur.UserId == userId && ur.RoomId == roomId);
            if (!isUserInRoom)
            {
                return Forbid("User is not a member of this room.");
            }

            var newMessage = new Message
            {
                MessageId = Guid.NewGuid(),
                RoomId = roomId,
                SenderId = userId,
                Content = message,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(newMessage);
            _context.SaveChanges();

            // 🔄 Notify All Clients Using SignalR
            var hubContext = HttpContext.RequestServices.GetRequiredService<IHubContext<ChatHub>>();
            hubContext.Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", userId.ToString(), message);

            return Ok();
        }


    }
}
