using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Discount
{
    public string DId { get; set; } = null!;

    public string? DName { get; set; }

    public string? DStart { get; set; }

    public string? DFinish { get; set; }

    public int? DPercent { get; set; }
}
