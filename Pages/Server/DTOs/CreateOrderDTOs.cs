using BlueStarMVC.Pages.Server.ZaloPayResults;
using BlueStarMVC.Pages.Server.ZaloPayHelper;
using BlueStarMVC.Pages.Server.ZaloPayResults;
using BlueStarMVC.Pages.Server;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;


namespace BlueStarMVC.Pages.Server.DTOs
{
    public class CreateOrderDTOs
    {
        public int AppId { get; set; }
        public string AppUser { get; set; }
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string AppTransId { get; set; }
        public string BankCode { get; set; }
        public EmbedData EmbedData { get; set; }
        public Item[] Item { get; set; }
        public string Description { get; set; }
        public string Mac { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string CallBackURL { get; set; }
        public CreateOrderDTOs(int appId, string appUser, long appTime, long amount, string appTransId
            , string bankCode, EmbedData embedData, Item[] item, string description,
            string phone, string email, string title, string callback)
        {
            AppId = appId;
            AppUser = appUser;
            AppTime = appTime;
            Amount = amount;
            AppTransId = appTransId;
            BankCode = bankCode;
            EmbedData = embedData;
            Item = item;
            Description = description;
            Phone = phone;
            Email = email;
            Title = title;
            CallBackURL = callback;
        }
        public void MakeSignature(string key1)
        {
            var data = AppId.ToString() + "|" + AppTransId + "|" + AppUser + "|" + Amount.ToString() + "|"
            + AppTime.ToString() + "|" + JsonConvert.SerializeObject(EmbedData) + "|" + JsonConvert.SerializeObject(Item);

            this.Mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data);
        }
        public Dictionary<string, string> GetContent()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            keyValuePairs.Add("app_id", AppId.ToString());
            keyValuePairs.Add("app_user", AppUser);
            keyValuePairs.Add("app_time", AppTime.ToString());
            keyValuePairs.Add("amount", Amount.ToString());
            keyValuePairs.Add("app_trans_id", AppTransId);
            keyValuePairs.Add("embed_data", JsonConvert.SerializeObject(EmbedData));
            keyValuePairs.Add("item", JsonConvert.SerializeObject(Item));
            keyValuePairs.Add("description", Description);
            keyValuePairs.Add("bank_code", "");
            keyValuePairs.Add("mac", Mac);
            keyValuePairs.Add("phone", Phone);
            keyValuePairs.Add("email", Email);
            keyValuePairs.Add("title", Title);
            keyValuePairs.Add("callback_url", CallBackURL);
            return keyValuePairs;
        }
        public async Task<CreateOrderResult> CreateOrderAsync(string create_order_url)
        {
            var content = new FormUrlEncodedContent(GetContent());
            var result = await HttpHelper.PostAsync<CreateOrderResult>(create_order_url, content);
            return result;
        }
    }
}