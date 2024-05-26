namespace BlueStarMVC.Pages.Server.DTOs
{
    public class QueryFlightDTOs
    {
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DepatureDay { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
    }
}
