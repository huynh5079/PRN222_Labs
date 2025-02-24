using System;
using System.Collections.Generic;

namespace PRN222_Lab5.Models;

public partial class Log
{
    public int LogId { get; set; }

    public string? LogType { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }
}
