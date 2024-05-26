using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.DTOs
{
    public class Data
    {
        [JsonPropertyName("app_id")]
        public int AppId { get; set; }

        [JsonPropertyName("app_trans_id")]
        public string AppTransId { get; set; }

        [JsonPropertyName("app_time")]
        public long AppTime { get; set; }

        [JsonPropertyName("app_user")]
        public string AppUser { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("embed_data")]
        public JsonElement EmbedDataString { get; set; }

        [JsonPropertyName("item")]
        public List<ItemResponse> Item { get; set; }

        [JsonPropertyName("zp_trans_id")]
        public long ZpTransId { get; set; }

        [JsonPropertyName("server_time")]
        public long ServerTime { get; set; }

        [JsonPropertyName("channel")]
        public int Channel { get; set; }

        [JsonPropertyName("merchant_user_id")]
        public string MerchantUserId { get; set; }

        [JsonPropertyName("zp_user_id")]
        public string ZpUserId { get; set; }

        [JsonPropertyName("user_fee_amount")]
        public int UserFeeAmount { get; set; }

        [JsonPropertyName("discount_amount")]
        public int DiscountAmount { get; set; }

    }
}