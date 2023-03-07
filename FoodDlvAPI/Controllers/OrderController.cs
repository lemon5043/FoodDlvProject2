using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Repositories;
using FoodDlvAPI.Services;
using FoodDlvAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodDlvAPI.Controllers
{
    public class OrderController : Controller
    {
        //Fields
        private readonly AppDbContext _context;
        private OrderService _orderService;
        private CartService _cartService;

        //Constructors
        public OrderController(AppDbContext context)
        {
            _context = context;
            IOrderRepository orderRepo = new OrderRepository(_context);
            

            this._orderService = new OrderService(orderRepo);
        }

        [HttpGet("OrderInfo")]
        public IActionResult OrderInfo(long cartId, string address, int fee)
        {
            var orderInfo = _orderService.OrderInfo(cartId, address, fee).ToOrderInfoVM();
            return Json(orderInfo);
        }

        [HttpPost("OrderEstablished")]
        public IActionResult OrderEstablished(long memberId, int storeId) 
        {
            _cartService.CheckOutCart(memberId, storeId);
            _orderService.CheckOutTime(storeId);


            return new EmptyResult();
        }

        [HttpGet("OrderTracking")]
        public IActionResult OrderTracking(long OrderId)
        {
            return View();
        }
    }
}
