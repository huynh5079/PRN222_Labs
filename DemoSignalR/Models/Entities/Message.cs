using System;

namespace DemoSignalR.Models.Entities
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; } // Sender of the message
        public Guid RoomId { get; set; } 
        public string Content { get; set; } = null!;
        public string MessageType { get; set; } = "text"; // text, image, file, etc.
        public DateTime SentAt { get; set; } = DateTime.UtcNow; // Timestamp when sent
        public bool IsRead { get; set; } = false; // If the message has been read

        public virtual ChatRoom Room { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
