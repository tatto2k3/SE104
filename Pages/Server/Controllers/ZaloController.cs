using BlueStarMVC.Pages.Server;
using BlueStarMVC.Pages.Server.DTOs;
using BlueStarMVC.Pages.Server.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/zalo")]
    [ApiController]
    public class ZaloController : ControllerBase
    {
        private readonly ZaloClient zaloClient;
        private readonly ZaloPayOptions zaloPayOptions;
        public ZaloController(ZaloClient zaloClient, IOptions<ZaloPayOptions> options)
        {
            this.zaloClient = zaloClient;
            zaloPayOptions = options.Value;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateTicketOrderDTO orders)
        {
            var result = await zaloClient.CreateOrderAsync(orders);
            return Ok(result);
        }
    }
}
