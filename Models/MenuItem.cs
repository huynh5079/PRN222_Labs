using System;
using System.Collections.Generic;

namespace PRN222_Lab5.Models;

public partial class MenuItem
{
    public int MenuItemId { get; set; }

    public int? RestaurantId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? AvailabilityStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Restaurant? Restaurant { get; set; }
}
