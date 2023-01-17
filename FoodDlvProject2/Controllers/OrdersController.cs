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
        private OrderService orderService;

        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;            
            IOrderRepository repo = new OrderRepository(_context);
            this.orderService = new OrderService(repo);
        }

		//GET: Orders
		[HttpGet]
        public async Task<IActionResult> Index(DateTime? dateStart, DateTime? dateEnd, string keyWord, int pageNumber = 1)
        {
			int pageSize = 5;
			pageNumber = pageNumber > 0 ? pageNumber : 1;

			var data = await orderService.SearchAsync(dateStart, dateEnd, keyWord);
            var dataAsync = data.Select(x => x.ToOrderVM()).ToPagedList(pageNumber, pageSize);

            return View(dataAsync);					

		}

		[HttpGet]
		//GET: OrderDetails
		public IActionResult DetailIndex(long Id)
		{
			var data = orderService.DetailSearch(Id)
				.Select(x => x.ToOrderDetailVM());

			return View(data);
		}

		[HttpGet]
		//GET: OrderProducts
		public IActionResult ProductDetails(long Id)
		{
			var data = orderService.ProductSearch(Id)
				.Select(x => x.ToOrderProductVM()).FirstOrDefault();
						
			return View(data);
		}			

		

	}
}

