using System;
using System.Collections.Generic;

namespace DemoSignalR.Models.Entities;

public partial class UserRoom
{
    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }

    public DateTime? JoinedAt { get; set; }

    public virtual ChatRoom Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
