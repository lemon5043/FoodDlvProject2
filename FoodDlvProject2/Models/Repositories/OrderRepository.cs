using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using X.PagedList;

namespace FoodDlvProject2.Models.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _context;

		public OrderRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<OrderMainDto> GetOrderMain(string revenueRange, string exceptionOrderRange, string completedOrderRange)
		{
			return null;
		}

		public async Task<IPagedList<OrderTrackingDto>> GetOrderTrackingAsync(DateTime? dateStart, DateTime? dateEnd,
															string searchItem, string keyWord,
															int pageSize, int pageNumber)
		{
			//連線到資料庫選取資料範圍
			IEnumerable<Order> query = _context.Orders
				.Include(o => o.Member)
				.Include(o => o.Store)
				.Include(o => o.OrderSchedules)
				.ThenInclude(os => os.Status);

			//日期範圍搜尋
			if (dateStart.HasValue)
			{
				query = query.Where(o => o.OrderSchedules.Any(os => os.MarkTime >= dateStart));
			}
			if (dateEnd.HasValue)
			{
				query = query.Where(o => o.OrderSchedules.Any(os => os.MarkTime <= dateEnd));
			}

			//關鍵字搜尋          
			if (string.IsNullOrEmpty(keyWord) == false)
			{
				switch (searchItem)
				{
					case "0":
						query = query.Where(o => o.Id.ToString().Contains(keyWord)
										|| (o.Member.LastName + o.Member.FirstName).Contains(keyWord)
										|| o.Store.StoreName.Contains(keyWord));
						break;

					case "Id":
						query = query.Where(o => o.Id.ToString().Contains(keyWord));
						break;

					case "MemberName":
						query = query.Where(o => (o.Member.LastName + o.Member.FirstName).Contains(keyWord));
						break;

					case "StoreName":
						query = query.Where(o => o.Store.StoreName.Contains(keyWord));
						break;
				}
			}

			//分頁處理
			pageNumber = pageNumber > 0 ? pageNumber : 1;

			//把搜尋結果轉換成Dto格式
			var data = query.Select(o => new OrderTrackingDto
			{
				Id = o.Id,
				MemberName = o.Member.LastName + o.Member.FirstName,
				StoreName = o.Store.StoreName,
				OrderTime = o.OrderSchedules.FirstOrDefault(os => os.StatusId == 1).MarkTime,
				DeliveryFee = o.DeliveryFee,
				Total = OrderDetailClac(o.Id) + o.DeliveryFee,
				OrderStatus = o.OrderSchedules.OrderBy(os => os.StatusId).LastOrDefault().Status.Status,
			});

			return await data.ToPagedListAsync(pageNumber, pageSize);
		}

		//計算單筆訂單明細
		private int OrderDetailClac(long id)
		{
			return _context.OrderDetails
				.Where(od => od.OrderId == id)
				.Select(od => od.UnitPrice * od.Count)
				.Sum();
		}


		public async Task<IEnumerable<OrderScheduleDto>> GetOrderScheduleAsync(long id)
		{
			var data = _context.OrderSchedules
				.Include(os => os.Status)
				.Include(os => os.Order)
				.ThenInclude(o => o.Store)
				.Include(os => os.Order)
				.ThenInclude(o => o.DeliveryDrivers)
				.Where(os => os.OrderId == id)
				.Select(os => new OrderScheduleDto
				{
					StoreId = os.Order.StoreId,
					DeliveryDriverId = os.Order.DeliveryDriversId,
					DeliveryAddress = os.Order.DeliveryAddress,
					ScheduleStatus = new OrderScheduleStatus
					{
						Status = os.Status.Status,
						StatusId = os.StatusId,
						MarkTime = os.MarkTime,
					}
				});

			return await data.ToListAsync();
		}


		public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailAsync(long id)
		{
			var data = _context.OrderDetails
				.Include(od => od.Product)
				.Where(od => od.OrderId == id)
				.Select(od => new OrderDetailDto
				{
					Id = od.Id,
					OrderId = od.OrderId,
					ProductId = od.ProductId,
					ProductName = od.Product.ProductName,
					UnitPrice = od.UnitPrice,
					Count = od.Count,
					SubTotal = OrderDetailClac(id)
				});

			return await data.ToListAsync();
		}

		public async Task<IEnumerable<OrderProductDetailDto>> GetOrderProductDetailAsync(long productId)
		{
			var data = _context.Products
				.Include(p => p.Store)
				.Where(p => p.Id == productId)
				.Select(p => new OrderProductDetailDto
				{
					Id = p.Id,
					StoreId = p.StoreId,
					StoreName = p.Store.StoreName,
					ProductName = p.ProductName,
					UnitPrice = p.UnitPrice,
					Photo = p.Photo,
					ProductContent = p.ProductContent,
				});

			return await data.ToListAsync();
		}
	}
}
