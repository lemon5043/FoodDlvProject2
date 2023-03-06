using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using FoodDlvAPI.Repositories;
using FoodDlvAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDlvAPI.Controllers
{
    public class OrderController : Controller
    {
        //Fields
        private readonly AppDbContext _context;
        private OrderService _orderService;

        //Constructors
        public OrderController(AppDbContext context)
        {
            _context = context;
            IOrderRepository orderRepo = new OrderRepository(_context);

            this._orderService = new OrderService(orderRepo);
        }

        [HttpGet("OrderInfo")]
        public IActionResult OrderInfo(CartDTO cart, OrderDTO order)
        {
            var orderInfo = _orderService.OrderInfo(cart, order).ToOrderVM();
            return Json(orderInfo);
        }

        [HttpPost("EstablishedOrder")]
        public IActionResult EstablishedOrder() 
        {
            return View();
        }
    }
}
