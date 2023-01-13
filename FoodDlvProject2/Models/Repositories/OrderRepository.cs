using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<OrderDto> Search(DateTime? start, DateTime? end, string keyWord)
        {
            IEnumerable<Order> query = _context.Orders
                .Include(o => o.Member)
                .Include(o => o.Store)
                .Include(o => o.OrderSchedules);                

            //日期範圍搜尋(未實作)

            //關鍵字搜尋
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => (x.Member.FirstName + x.Member.LastName).Contains(keyWord) || x.Store.StoreName.Contains(keyWord));
            }


            return query.Select(o => new OrderDto
            {
                Id = o.Id,
                MemberName = o.Member.LastName + o.Member.FirstName,
                StoreName = o.Store.StoreName,
				//OrderTime = o.OrderSches.FirstOrDefault(x => x.StatusId == 1).MarkTime,
				orderSchedule = o.OrderSchedules.Select(x => new OrderSchedule
                {                    
                    StatusId = x.StatusId,
                    MarkTime = x.MarkTime,                    
                }),
                DeliveryAddress = o.DeliveryAddress,
                DeliveryFee = o.DeliveryFee,
                Total = OrderDetailClac(o.Id) + o.DeliveryFee,
            });           
                       
        }

        private int OrderDetailClac(long id)
        {
            return _context.OrderDetails
                .Where(od => od.OrderId == id)
                .Select(n => n.UnitPrice * n.Count)
                .Sum();            
        }

        public IEnumerable<OrderDetailDto> DetailSearch(long orderId)
        {
            IEnumerable<OrderDetailDto> query = _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.OrderId == orderId)
                .Select(x => new OrderDetailDto
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
					ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
					UnitPrice = x.UnitPrice,
					Count = x.Count,
					SubTotal = OrderDetailClac(orderId)
				});
                
               return query;
        }

        //public IEnumerable<OrderDetail> Read()
        //{
        //    IEnumerable<OrderDetail> query = _context.OrderDetails;
        //}
        //public  Read()
        //{
        //    var data = _context.Orders
        //        .Include(x => x.OrderDetails)
        //        .ThenInclude(p => p.Product)
        //        .Include(os => os.OrderSchedules)
        //        .ToList()
        //        .Select(x => new OrderVM
        //        {
        //            Id = x.Id,
        //            MemberId = x.MemberId,
        //            StoreId = x.StoreId,
        //            OrderTime = x.OrderSchedules
        //            .FirstOrDefault(x => x.StatusId == 1)
        //            .MarkTime,
        //            Items = x.OrderDetails
        //                .Select(y => new OrderDetailVM
        //                {
        //                    Id = y.Id,
        //                    OrderId = y.OrderId,
        //                    ProductId = y.ProductId,
        //                    ProductName = y.Product.ProductName,
        //                    UnitPrice = y.UnitPrice,
        //                    Count = y.Count,
        //                }).ToList(),
        //        }).ToList();

        //    return data;
        //}
    }
}
