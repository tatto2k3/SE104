using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Plane
{
    public string PlId { get; set; } = null!;

    public string? Typeofplane { get; set; }

    public int? BusinessCapacity { get; set; }

    public int? EconomyCapacity { get; set; }
}
