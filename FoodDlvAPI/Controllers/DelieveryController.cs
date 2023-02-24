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

        [HttpPost]
        public async Task Online(int dirverId)
        {
            deliveryService.ChangeWorkingStatus(dirverId);

            //建立群組
            //string role = "driver";
            //string groupId = role + dirverId.ToString();
            //await _hubContext.Clients.;
        }

        //[HttpPost]
        //public async Task Offline(int dirverId)
        //{
        //    deliveryService.ChangeToOffline(dirverId);

        //    //離開群組
        //    //string role = "driver";
        //    //string groupId = role + dirverId.ToString();
        //    //await _hubContext.Groups.RemoveFromGroupAsync(groupId, role);
        //}

        //訂單指派，商家完成訂單後觸發
        //像前端發送請求
        //AasignmentOrderVM傳送簡單的訂單資訊
        //[HttpGet("{orderId}")]可能是?
        public async Task<AasignmentOrderVM> OrderAasignment(int orderId)
        {
            var data = await deliveryService.GetOrderDetail(orderId);
            return data.ToAasignmentOrderVM();
        }

        //
        //接受or取消請求
        [HttpGet("{reply}/{orderId}")]
        public async Task<AasignmentOrderVM> OrderAasignment(bool reply, int orderId)
        {

            //接受訂單
            if (reply)
            {
                var data = await deliveryService.NavigationToStore(orderId);
                return data.ToAasignmentOrderVM();
            }
            //取消接單
            //todo 通知店家重新尋單
            //await _hubContext.Groups()

        }

        [HttpGet("{orderId}")]
        public async Task<string> MealConfirmation(int orderId)
        {
            var data = deliveryService.NavigationToCustomer(orderId);
            return await data;
        }

        public async Task DeliveryArrive(int orderId)
        {
            //todo 回報給客戶
            deliveryService.OrderArrive(orderId);

        }

        //todo 未能達成外送?


        public async Task CancelOrder(CancellationVM cancellation)
        {
            //todo 回報給店家
        }
    }
}
