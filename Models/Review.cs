using System;
using System.Collections.Generic;

namespace PRN222_Lab5.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? RestaurantId { get; set; }

    public int? DeliveryDriverId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual DeliveryDriver? DeliveryDriver { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual User? User { get; set; }
}
