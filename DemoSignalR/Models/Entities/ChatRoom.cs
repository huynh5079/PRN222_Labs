using System;
using System.Collections.Generic;

namespace DemoSignalR.Models.Entities;

public partial class ChatRoom
{
    public Guid RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserRoom> UserRooms { get; set; } = new List<UserRoom>();
}
