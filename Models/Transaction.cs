using System;
using System.Collections.Generic;

namespace PRN222_Lab5.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionStatus { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Order? Order { get; set; }
}
