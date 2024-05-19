using System;
using System.Collections.Generic;

namespace BlueStarMVC.Models;

public partial class Ticket
{
    public string TId { get; set; } = null!;

    public string Cccd { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FlyId { get; set; } = null!;

    public string Seat_Type_ID { get; set; } = null!;

    public long TicketPrice { get; set; }

    public string SDT { get; set; } = null!;

}
