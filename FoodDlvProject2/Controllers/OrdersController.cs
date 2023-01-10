using FoodDlvProject2.Models.BackSideVM;
using FoodDlvProject2.Models.EFModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvProject2.Controllers
{
	public class OrdersController : Controller
	{
		private readonly AppDbContext _context;

		public OrdersController(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{			
            var data = await _context.Orders
                .Include(x => x.OrderDetails)                
                .Include(os => os.OrderSchedules)
                .Select(x => new OrderVM
                {
                    Id = x.Id,
                    MemberId = x.MemberId,
                    StoreId = x.StoreId,
                    OrderTime = x.OrderSchedules
                    .FirstOrDefault(x => x.StatusId == 1)
                    .MarkTime,
                })
            //.Select(x => new OrderDetailVM
            //{
            //	ProductId = x.ProductId,
            //	ProductName = x.ProductName,
            //	UnitPrice = x.UnitPrice,
            //	Count = x.OrderDetailVM.Count,
            //});
                .ToListAsync();
            return View(data);
		}


	}
}
