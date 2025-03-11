using System;
using System.Collections.Generic;

namespace DemoSignalR.Models.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserRoom> UserRooms { get; set; } = new List<UserRoom>();
}
