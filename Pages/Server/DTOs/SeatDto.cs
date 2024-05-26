namespace Blue_star.Pages.Server.DTOs
{
    public class SeatDto
    {
        public string SeatId { get; set; } = null!;
        public string? SeatType { get; set; }
        public string? FlightId { get; set; }
        public bool? Isbooked { get; set; }
    }
}