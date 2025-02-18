using System;
using System.Collections.Generic;

namespace ShopeeFood4.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? RestaurantId { get; set; }

    public int? DeliveryDriverId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? OrderStatus { get; set; }

    public string? DeliveryAddress { get; set; }

    public DateTime? PlacedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual DeliveryDriver? DeliveryDriver { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Restaurant? Restaurant { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
