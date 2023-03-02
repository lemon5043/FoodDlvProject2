using FoodDlvAPI.Hubs;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol;
using System.Data;

namespace FoodDlvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelieveryController : Controller
    {
        private readonly DeliveryService deliveryService;

        private readonly IHubContext<OrderHub> _hubContext;

        public DelieveryController(IHubContext<OrderHub> hubContext)
        {
            var db = new AppDbContext();
            IDeliveryRepository repository = new DeliveryRepository(db);
            this.deliveryService = new DeliveryService(repository);
            this._hubContext = hubContext;
        }

        [HttpPut("ChangeWorkingStatus/{dirverId}")]
        public async Task Online(int dirverId)
        {
            deliveryService.ChangeWorkingStatus(dirverId);

            //建立群組
            //string role = "driver";
            //string groupId = role + dirverId.ToString();
            //await _hubContext.Clients.;
        }


        //訂單指派，商家完成訂單後觸發
        //像前端發送請求
        //AasignmentOrderVM只回傳店家地址及OrderId避免外送員挑單
        [HttpGet("OrderAasignment")] //可能是?
        public async Task<AasignmentOrderVM> OrderAasignment(int orderid)
        {
            var data = await deliveryService.GetOrderDetail(orderid);
            return data.ToAasignmentOrderVM();
        }
       
        //接受or取消請求
        [HttpGet("{reply}/{orderId}")]
        public async Task<AasignmentOrderVM> OrderAasignment(bool reply, int orderId)
        {

            //接受訂單
            if (reply)
            {
                await deliveryService.MarkOrderStatus(orderId);
                var data = await deliveryService.NavigationToStore(orderId);
                return data.ToAasignmentOrderVM();
            }
            //取消接單
            //todo 通知店家重新尋單
            //await _hubContext.Groups()
            return null;
        }

        //確認訂單開始外送
        [HttpGet("{orderId}")]
        public async Task<string> MealConfirmation(int orderId)
        {
            var data = deliveryService.NavigationToCustomer(orderId);
            return await data;

            //todo 訂單與餐點不符取消接單?
            //await _hubContext.Groups()
        }

        [HttpPut("{orderId}")]
        public async Task DeliveryArrive(int orderId)
        {
            await deliveryService.MarkOrderStatus(orderId);
            //todo 回報給客戶
           

        }

        ////todo 未能達成外送?


        //public async Task CancelOrder(CancellationVM cancellation)
        //{
        //    //todo 回報給店家
        //}
    }
}
