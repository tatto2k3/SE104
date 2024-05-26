using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.DTOs
{
    public class CallBackParameter
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("mac")]
        public string Mac { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }
    }
}