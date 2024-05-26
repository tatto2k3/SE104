using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.DTOs
{
    public class Item
    {
        [JsonPropertyName("item_id")] public string ItemID { get; set; }
        [JsonPropertyName("item_name")] public string ItemName { get; set; }

    }
}