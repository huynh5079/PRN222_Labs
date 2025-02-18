using System;
using System.Collections.Generic;

namespace ShopeeFood4.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public int? RestaurantId { get; set; }

    public int? DeliveryDriverId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual DeliveryDriver? DeliveryDriver { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual User? User { get; set; }
}
