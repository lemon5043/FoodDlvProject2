//using FoodDlvAPI.Hubs;
//using FoodDlvAPI.Models;
//using FoodDlvAPI.Models.Repositories;
//using FoodDlvAPI.Models.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using System.Data;

//namespace FoodDlvAPI.Controllers
//{
//    public class DelieveryController : Controller
//    {
//        private readonly DeliveryService deliveryService;

//        private readonly IHubContext<OrderHub> _hubContext;

//        public DelieveryController(IHubContext<OrderHub> hubContext)
//        {
//            var db = new AppDbContext();
//            IDeliveryRepository repository = new DeliveryRepository(db);
//            this.deliveryService = new DeliveryService(repository);
//            this._hubContext = hubContext;
//        }

//        [HttpPost]
//        public async Task Online(int dirverId)
//        {
//            deliveryService.ChangeToOnline(dirverId);
            
//            //建立群組
//            string role = "driver";
//            string groupId = role + dirverId.ToString();
//            await _hubContext.Clients.;
//        }

//        [HttpPost]
//        public async Task Offline(int dirverId)
//        {
//            deliveryService.ChangeToOffline(dirverId);
            
//            //離開群組
//            string role = "driver";
//            string groupId = role + dirverId.ToString();
//            await _hubContext.Groups.RemoveFromGroupAsync(groupId, role);
//        }


//        public async Task<AasignmentOrderVM> OrderAasignment(int orderId)
//        {
//            return deliveryService.GetOrderDetail(orderId).ToAasignmentOrderVM();
//        }

//        public async Task<JsonResult> OrderAasignment(bool reply, int orderId)
//        {
//            if (reply)
//            {
//                deliveryService.OrderAcceptedNavigationToStore(orderId);
//                return;
//            }
//            //todo 回報給店家
//            return null;
//        }

//        public async Task<JsonResult> MealConfirmation(bool reply, int orderId)
//        {
//            if (reply)
//            {
//                deliveryService.OrderAcceptedNavigationToCustomer(orderId);
//                return;
//            }
//            //todo 回報給店家
//            return null;
//        }

//        public async Task DeliveryArrive(bool reply, int orderId)
//        {
//            if (reply)
//            {
//                //todo 回報給客戶
//                deliveryService.OrderArrive(orderId);

//            }
//            //todo 回報給客戶
//        }

//        public async Task CancelOrder(CancellationVM cancellation)
//        {
//            //todo 回報給店家
//        }
//    }
//}
