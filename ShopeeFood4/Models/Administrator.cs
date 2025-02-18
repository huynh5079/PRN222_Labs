using System;
using System.Collections.Generic;

namespace ShopeeFood4.Models;

public partial class Administrator
{
    public int AdminId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
