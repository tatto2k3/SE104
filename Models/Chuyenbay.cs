using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueStarMVC.Models
{
    public partial class Chuyenbay
    {
        public string FlyId { get; set; } = null!;
        public int? OriginalPrice { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? DepartureTime { get; set; }
        public string? DepartureDay { get; set; }
        public string? FlightTime { get; set; } // Thời gian bay
        public int? SeatEmpty { get; set; } = Storage.Seat1Before + Storage.Seat2Before;
        public int? SeatBooked { get; set; } = 0;
    }
}
