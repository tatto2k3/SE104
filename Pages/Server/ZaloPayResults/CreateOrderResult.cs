using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.ZaloPayResults
{
    public class CreateOrderResult
    {
        [JsonPropertyName("return_code")] public int ReturnCode { get; set; }
        [JsonPropertyName("return_message")] public string ReturnMessage { get; set; }
        [JsonPropertyName("sub_return_code")] public int SubReturnCode { get; set; }
        [JsonPropertyName("sub_return_message")] public string SubReturnMessage { get; set; }
        [JsonPropertyName("order_url")] public string OrderUrl { get; set; }
        [JsonPropertyName("zp_trans_token")] public string ZpTransToken { get; set; }
        [JsonPropertyName("order_token")] public string OrderToken { get; set; }


    }
}