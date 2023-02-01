using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;

namespace FoodDlvProject2.Controllers
{
    public class OrdersController : Controller
    {
		//Field
        private OrderService orderService;
        private readonly AppDbContext _context;

		//Constructor
        public OrdersController(AppDbContext context)
        {
            _context = context;            
            IOrderRepository repo = new OrderRepository(_context);
            this.orderService = new OrderService(repo);
        }

		//OrderMain(未完成)
		public async Task<IActionResult> OrderMain(string revenueRange, string exceptionOrderRange, string completedOrderRange)
		{
			var data = await orderService.OrderMain(revenueRange, exceptionOrderRange, completedOrderRange);
			return View();
		}

		//OrderTracking
		[HttpGet]
        public async Task<IActionResult> OrderTracking(DateTime? dateStart, DateTime? dateEnd, 
														string searchItem, string keyWord, 
														int pageSize = 5 ,int pageNumber = 1)
        {
			
			ViewBag.SearchItem = orderService.GetOrderTrackingSearchOptions(searchItem);
			ViewBag.KeyWord = keyWord;
			ViewBag.PageSize = pageSize;

			var data = (await orderService.OrderTrackingAsync(dateStart, dateEnd, searchItem, keyWord, pageSize, pageNumber))
				.Select(ot => ot.ToOrderTrackingVM());

            return View(data);					

		}

		//OrderTracking-OrderSchedule
		public async Task<IActionResult> OrderSchedule(long id)
		{
			var data = (await orderService.OrderScheduleAsync(id));

			return Json(data);
		}



		[HttpGet]
		//GET: OrderDetails(待修改)
		public IActionResult DetailIndex(long Id)
		{
			var data = orderService.DetailSearch(Id)
				.Select(x => x.ToOrderDetailVM());

			return View(data);
		}

		[HttpGet]
		//GET: OrderProducts(待修改)
		public IActionResult ProductDetails(long Id)
		{
			var data = orderService.ProductSearch(Id)
				.Select(x => x.ToOrderProductVM()).FirstOrDefault();
						
			return View(data);
		}			

		

	}
}

