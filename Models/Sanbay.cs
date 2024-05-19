using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueStarMVC.Models;

public partial class Sanbay
{
    public string AirportId { get; set; } = null!;

    public string? AirportName { get; set; }

    public string? Place { get; set; }

}
