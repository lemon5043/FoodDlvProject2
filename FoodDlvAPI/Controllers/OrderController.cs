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
            ICartRepository cartRepo = new CartRepository(_context);
            

            this._orderService = new OrderService(orderRepo, cartRepo);
        }

        [HttpGet("OrderInfo")]
        public IActionResult OrderInfo(long cartId, string address, int fee)
        {
            try
            {
                var orderInfo = _orderService.OrderInfo(cartId, address, fee).ToOrderInfoVM();
                return Json(orderInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("OrderEstablished")]
        public IActionResult OrderEstablished(int memberId, int storeId, int fee ,string address) 
        {
            try
            {
                _cartService.CheckOutCart(memberId, storeId);
                _orderService.CheckOutTime(storeId);
                _orderService.OrderEstablished(memberId, storeId, fee, address);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("OrderTracking")]
        public IActionResult OrderTracking(long orderId)
        {
            try
            {
                var orderTracking = _orderService.OrderTracking(orderId);
                return Json(orderTracking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
