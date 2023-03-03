using FoodDlvAPI.Hubs;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Authorize(Policy = "driverOnly")]
        [HttpPut("ChangeWorkingStatus/{dirverId}")]
        public async Task Online(int dirverId)
        {
            try
            {
                deliveryService.ChangeWorkingStatus(dirverId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

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
            try
            {
                var data = await deliveryService.GetOrderDetail(orderid);
                return data.ToAasignmentOrderVM();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //接受訂單請求
        //外送員工作狀態改變
        [HttpGet("OrderAccept/{orderId}/{DriverId}")]
        public async Task<AasignmentOrderVM> OrderAccept(int orderId, int driverId)
        {
            try
            {
                var data = await deliveryService.UpdateOrder(orderId, driverId);
                return data.ToAasignmentOrderVM();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //取消訂單請求
        //回傳取消原因
        [HttpGet("Cancellation")]
        public async Task<IEnumerable<DriverCancellationsVM>> GetCancellationReason()
        {
            try
            {
                var data = await deliveryService.GetListAsync();
                return data.Select(x => x.ToDriverCancellationsVM());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //外送員回報取消原因
        [HttpPost("Cancellation")]
        public async Task<ActionResult<string>> OrderDecline(DriverCancellationRecordsVM driverCancellation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await deliveryService.SaveCancellationRecord(driverCancellation.ToDriverCancellationRecordsDTO());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        //確認訂單開始外送
        [HttpGet("{orderId}")]
        public async Task<string> MealConfirmation(int orderId)
        {
            try
            {
                var data = deliveryService.NavigationToCustomer(orderId);
                return await data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //餐點送達回報紀錄
        //外送員工作狀態改變
        [HttpPut("{orderId}/{DriverId}")]
        public async Task DeliveryArrive(int orderId, int driverId)
        {
            try
            {
                await deliveryService.MarkOrderStatus(orderId, driverId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
