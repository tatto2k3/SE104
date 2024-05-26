using BlueStarMVC.Pages.Server.DTOs;
using BlueStarMVC.Pages.Server.Options;
using BlueStarMVC.Pages.Server.ZaloPayResults;
using BlueStarMVC.Pages.Server.ZaloPayHelper;
using Microsoft.Extensions.Options;


namespace BlueStarMVC.Pages.Server
{
    public class ZaloClient
    {
        private readonly ZaloPayOptions zaloPayOptions;
        private readonly ILogger<ZaloClient> logger;
        public ZaloClient(IOptions<ZaloPayOptions> options, ILogger<ZaloClient> logger)
        {
            zaloPayOptions = options.Value;
            this.logger = logger;
        }
        public async Task<CreateOrderResult> CreateOrderAsync(CreateTicketOrderDTO createOrder)
        {
            Random rnd = new Random();
            var app_trans_id = rnd.Next(1000000); // Generate a random order's ID.

            Console.WriteLine("appid: ", zaloPayOptions.Appid);

            CreateOrderDTOs orderZaloPay = new CreateOrderDTOs(int.Parse(zaloPayOptions.Appid),createOrder.CustomerName,Utils.GetTimeStamp(),(long)createOrder.TicketAmount,  // Chuyển đổi từ int sang long
    DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id,
    " ", new EmbedData
            {
                RedirectURL = "https://56ee-115-78-131-128.ngrok-free.app",
                CustomerEmail = createOrder.CustomerEmail,
                CustomerIdentify = createOrder.CustomerIdentify,
                CustomerName = createOrder.CustomerName,
                CustomerPhone = createOrder.CustomerPhone,
                DepeartureDay = createOrder.DepeartureDay,
                DepeartureTime = createOrder.DepeartureTime,
                ArriveDay = createOrder.ArriveDay,
                SeatID = createOrder.SeatID,
                FlightID = createOrder.FlightID,
                DurationTime = createOrder.DurationTime,
                TripType = createOrder.TripType
            },
            new Item[] { new Item { ItemID = "", ItemName = "" } }, $"Creat Ticket Order for {createOrder.CustomerName}",
             createOrder.CustomerPhone, createOrder.CustomerEmail, "Thanh toan tien ve may bay", zaloPayOptions.CallbackUrl);
            orderZaloPay.MakeSignature(zaloPayOptions.Key1);
            var result = await orderZaloPay.CreateOrderAsync(zaloPayOptions.Endpoint);
            return result;
        }

    }
}