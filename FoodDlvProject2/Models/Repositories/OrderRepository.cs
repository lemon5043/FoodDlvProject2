using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace FoodDlvProject2.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<OrderMainDto> GetOrderMain(string revenueRange, string exceptionOrderRange, string completedOrderRange)
        {
            return null;
        }
                
        public Task<IEnumerable<OrderDto>> SearchAsync(DateTime? start, DateTime? end, string keyWord)
        {
            IEnumerable<Order> query = _context.Orders
                .Include(o => o.Member)
                .Include(o => o.Store)
                .Include(o => o.OrderSchedules)
                .ThenInclude(os => os.Status);

            //日期範圍搜尋
            if (start.HasValue)
            {
                query = query.Where(o => o.OrderSchedules.Any(os => os.MarkTime >= start));
                    
            }
            if (end.HasValue)
            {
                query = query.Where(o => o.OrderSchedules.Any(os => os.MarkTime <= end));
            }

            //關鍵字搜尋
            if (!string.IsNullOrEmpty(keyWord))
            {
                 query = query.Where(o => (o.Member.LastName + o.Member.FirstName).Contains(keyWord)
                    || o.Store.StoreName.Contains(keyWord)
                    || o.DeliveryAddress.Contains(keyWord));
            }


            return Task.FromResult(query.Select(o => new OrderDto
            {
                Id = o.Id,
                MemberName = o.Member.LastName + o.Member.FirstName,
                StoreName = o.Store.StoreName,
				OrderTime = o.OrderSchedules.FirstOrDefault(x => x.StatusId == 1).MarkTime,
				orderSchedule = o.OrderSchedules
                    .OrderBy(os => os.StatusId)
                    .Select(os => new OrderScheduleDto
                    {                     
                        StatusId = os.StatusId,
                        Status = os.Status.Status,
                        MarkTime = os.MarkTime,                    
                    }).ToList(),
                DeliveryAddress = o.DeliveryAddress,
                DeliveryFee = o.DeliveryFee,
                Total = OrderDetailClac(o.Id) + o.DeliveryFee,
            }));           
                       
        }       

        //計算單筆訂單明細
        private int OrderDetailClac(long id)
        {
            return _context.OrderDetails
                .Where(od => od.OrderId == id)
                .Select(od => od.UnitPrice * od.Count)
                .Sum();            
        }


        public IEnumerable<OrderDetailDto> DetailSearch(long orderId)
        {
            IEnumerable<OrderDetailDto> query = _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.OrderId == orderId)
                .Select(od => new OrderDetailDto
                {
                    Id = od.Id,
                    OrderId = od.OrderId,
					ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
					UnitPrice = od.UnitPrice,
					Count = od.Count,
					SubTotal = OrderDetailClac(orderId)
				});
                
               return query;
        }
        
        public IEnumerable<OrderProductDto> ProductSearch(long productId)
        {
            IEnumerable<OrderProductDto> query = _context.Products
                .Include(p => p.Store)
                .Where(p => p.Id == productId)
                .Select(p => new OrderProductDto
                {
                    Id = p.Id,
					StoreId = p.StoreId,
					StoreName = p.Store.StoreName,
					ProductName = p.ProductName,
					UnitPrice = p.UnitPrice,
					Photo = p.Photo,
					ProductContent = p.ProductContent,
				});

            return query;
        }
    }
}
