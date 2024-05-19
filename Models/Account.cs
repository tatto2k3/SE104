using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Name { get; set; }

}
