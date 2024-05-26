using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.ZaloPayResults
{
    public class CallBackResult
    {
        [JsonPropertyName("return_code")] public int ReturnCode { get; set; }
        [JsonPropertyName("return_message")] public string ReturnMessage { get; set; }
    }
}
