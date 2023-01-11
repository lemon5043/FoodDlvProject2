using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
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

        //GET(list): Orders
		public async Task<IActionResult> Index()
		{
            var data1 = await _context.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(p => p.Product)
                .Include(os => os.OrderSchedules)
                .ToListAsync();
                

            var data = data1
                .Select(x => new OrderVM
                {
                    Id = x.Id,
                    MemberId = x.MemberId,
                    StoreId = x.StoreId,
                    OrderTime = x.OrderSchedules
                    .FirstOrDefault(x => x.StatusId == 1)
                    .MarkTime,
                    Items = x.OrderDetails
                        .Select(y => new OrderDetailVM
                        {
                            Id = y.Id,
                            OrderId = y.OrderId,
                            ProductId = y.ProductId,
                            ProductName = y.Product.ProductName,
                            UnitPrice = y.UnitPrice,
                            Count = y.Count,                            
                        }).ToList(),

                }).ToList();
            return View(data);
		}

        //GET(list): Orders/OrderDetails/5
        public async Task<IActionResult> Details(long id)
        {
            var orderDetail = _context.OrderDetails
                .Include(p => p.Product)
                .Where(x => x.Id == id)
                .Select(x => new OrderDetailVM
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    UnitPrice = x.UnitPrice,
                    Count = x.Count,
                });

            return View(await orderDetail.ToListAsync());
        }

        //GET(detail): Orders/OrderDetails/Products/5
        public async Task<IActionResult> ProductsDetail(long id)
        {
            var product = await _context.Products
                .Include(s => s.Store)              
                .Select(x => new
                {
                    Id = x.Id,
                    StoreId = x.StoreId,
                    ProductName = x.ProductName,
                    Photo = x.Photo,
                    ProductContent = x.ProductContent,
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            return View(product);
        }


        //GET(delete): Orders/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var order = await _context.Orders
                .Include(os => os.OrderSchedules)
                .Include(od => od.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(order);
        }

        //POST(delete): Order/Delete/5
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            return View();
        }
    }

}

