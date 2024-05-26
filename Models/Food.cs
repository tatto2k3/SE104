using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Food
{
    public string FId { get; set; } = null!;

    public string? FName { get; set; }

    public string? FPrice { get; set; }
}
