using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(DateTime? dateStart, DateTime? dateEnd, string keyWord, int pageNumber = 1)
        {
			int pageSize = 5;
			pageNumber = pageNumber > 0 ? pageNumber : 1;

			var data = await orderService.SearchAsync(dateStart, dateEnd, keyWord);
            var dataAsync = data.Select(x => x.ToOrderVM()).ToPagedList(pageNumber, pageSize);

            return View(dataAsync);					

		}


		//GET: OrderDetails
		public IActionResult DetailIndex(long Id)
		{
			var data = orderService.DetailSearch(Id)
				.Select(x => x.ToOrderDetailVM());

			return View(data);
		}


		//GET: OrderProducts
		public IActionResult ProductIndex(long ProductId)
		{
			var data = orderService.ProductSearch(ProductId)
				.Select(x => x.ToOrderProductVM());

			return View(data);
		}

		
		

		//GET(list): Orders/OrderDetails/5
		//public async Task<IActionResult> Details(long id)
		//{
		//    var orderDetail = _context.OrderDetails
		//        .Include(p => p.Product)
		//        .Where(x => x.Id == id)
		//        .Select(x => new OrderDetailVM
		//        {
		//            ProductId = x.ProductId,
		//            ProductName = x.Product.ProductName,
		//            UnitPrice = x.UnitPrice,
		//            Count = x.Count,
		//        });

		//    return View(await orderDetail.ToListAsync());
		//}

		//GET(detail): Orders/OrderDetails/Products/5
		//public async Task<IActionResult> ProductsDetail(long id)
		//{
		//    var product = await _context.Products
		//        .Include(s => s.Store)              
		//        .Select(x => new
		//        {
		//            Id = x.Id,
		//            StoreId = x.StoreId,
		//            ProductName = x.ProductName,
		//            Photo = x.Photo,
		//            ProductContent = x.ProductContent,
		//        })
		//        .FirstOrDefaultAsync(x => x.Id == id);
		//    return View(product);
		//}


		//GET(delete): Orders/Delete/5
		//public async Task<IActionResult> Delete(long id)
		//{
		//    var order = await _context.Orders
		//        .Include(os => os.OrderSchedules)
		//        .Include(od => od.OrderDetails)
		//        .FirstOrDefaultAsync(x => x.Id == id);

		//    return View(order);
		//}

		////POST(delete): Order/Delete/5
		//public async Task<IActionResult> DeleteConfirmed(long id)
		//{
		//    return View();
		//}

	}
}

