using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.DTOs
{
    public class ItemResponse
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerIdentify { get; set; }
        public string SeatID { get; set; }
        public string FlightID { get; set; }
        public string DepeartureDay { get; set; }
        public string ArriveDay { get; set; }
        public string DepeartureTime { get; set; }
        public string ArriveTime { get; set; }
        public string CustomerPhone { get; set; }
    }
}
