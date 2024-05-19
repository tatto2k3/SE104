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
        public int? SeatEmpty { get; set; } // Số lượng ghế hạng 1
        public int? SeatBooked { get; set; } // Số lượng ghế hạng 2
    }
}
