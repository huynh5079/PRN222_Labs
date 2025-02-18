using System;
using System.Collections.Generic;

namespace ShopeeFood4.Models;

public partial class Log
{
    public int LogId { get; set; }

    public string? LogType { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }
}
