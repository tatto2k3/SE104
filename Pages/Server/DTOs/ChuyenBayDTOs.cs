namespace BlueStarMVC.Pages.Server.DTOs
{
    public class ChuyenbayDTO
    {
        public string FlyId { get; set; }
        public int? OriginalPrice { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? DepartureTime { get; set; }
        public string? DepartureDay { get; set; }
        public string? FlightTime { get; set; }
        public int? SeatEmpty { get; set; }
        public int? SeatBooked { get; set; }
        public List<string> TrungGian { get; set; } 
    }

}
