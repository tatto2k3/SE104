using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.DTOs
{
    public class CreateTicketOrderDTO
    {
        [JsonPropertyName("ticket_amount")] public long TicketAmount { get; set; }
        [JsonPropertyName("customer_name")] public string CustomerName { get; set; }
        [JsonPropertyName("customer_email")] public string CustomerEmail { get; set; }
        [JsonPropertyName("customer_identify")] public string CustomerIdentify { get; set; }
        [JsonPropertyName("seat_id")] public string SeatID { get; set; }
        [JsonPropertyName("flight_id")] public string FlightID { get; set; }
        [JsonPropertyName("departure_day")] public string DepeartureDay { get; set; }
        [JsonPropertyName("arrive_day")] public string ArriveDay { get; set; }
        [JsonPropertyName("departure_time")] public string DepeartureTime { get; set; }
        [JsonPropertyName("arrive_time")] public string ArriveTime { get; set; }
        [JsonPropertyName("customer_phone")] public string CustomerPhone { get; set; }
        [JsonPropertyName("duration_time")] public string DurationTime { get; set; }
        [JsonPropertyName("trip_type")] public string TripType { get; set; }

    }
}