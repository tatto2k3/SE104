using System.Text.Json.Serialization;

namespace BlueStarMVC.Pages.Server.DTOs
{
    public class EmbedDataResponse
    {
        [JsonPropertyName("RedirectURL")] public string redirectUrl { get; set; }

    }
}