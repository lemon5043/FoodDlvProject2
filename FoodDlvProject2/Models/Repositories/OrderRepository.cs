using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FoodDlvProject2.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderVM> Search()
        {
            var query = _context.Orders
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    MemberName = o.Member.LastName + o.Member.FirstName,
                    StoreName = o.Store.StoreName,
                    OrderTime = o.OrderSchedules.SingleOrDefault(x => x.StatusId == 1).MarkTime,
                    DeliveryFee = o.DeliveryFee,
                    Total = o.OrderDetails.Where(od => od.OrderId == o.Id).Select(n => n.UnitPrice * n.Count).Sum()
                }).Select(x => new OrderVM
                {
                    Id = x.Id,
                    MemberName = x.MemberName,
                    StoreName = x.StoreName,
                    OrderTime = x.OrderTime,
                    DeliveryFee = x.DeliveryFee,
                    total = x.Total,
                });

            //if(string.IsNullOrEmpty(keyWord)) query = query.Where(x => x.MemberName.Contains(keyWord));


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
