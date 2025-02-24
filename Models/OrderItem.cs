using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222_Lab5.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    [Required]
    public int? MenuItemId { get; set; }

    [Required]
    public int? Quantity { get; set; }

    [Required]
    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MenuItem? MenuItem { get; set; }

    public virtual Order? Order { get; set; }
}
