using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlueStarMVC.Pages.Server.ZaloPayHelper
{
    public class HttpHelper
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<T> PostAsync<T>(string uri, HttpContent content)
        {
            var response = await httpClient.PostAsync(uri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseString);
            return result;
        }


    }
}