using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        //Fields
        private readonly AppDbContext _context;

        //Constructors
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrderDTO GetOrderInfo(long cartId, string address, int fee)
        {
            var orderInfo = new OrderDTO()
            {
                Cart = _context.Carts.First(c => c.Id == cartId).ToCartDTO(),
                DeliveryAddress = address,
                DeliveryFee = fee,
            };
            return orderInfo;
        }

        public void CheckOutTime(int storeId)
        {
            var nowTime = DateTime.Now.TimeOfDay;
            int nowDay = Convert.ToInt32(DateTime.Now.DayOfWeek);

            var targetStore = _context.StoreBusinessHours.Where(sbh => sbh.StoreId == storeId).ToList();
            var timeRange = targetStore.FirstOrDefault(sbh => sbh.OpeningDays == nowDay && sbh.OpeningTime <= nowTime && sbh.ClosingTime <= nowTime);

            if (timeRange == null)
            {
                throw new Exception("目前非商家營業時間");
            }
        }

        public void CashTransfer(int memberId, int storeId, int fee)
        {
            int memberWallet = _context.Members.First(m => m.Id == memberId).Balance;
            var cart = _context.Carts.First(c => c.MemberId == memberId && c.StoreId == storeId);
            var identifyGroup = cart.CartDetails.GroupBy(d => d.IdentifyNum).ToList();
            int cartTotal = fee;

            foreach (var group in identifyGroup)
            {
                var product = _context.Products.First(p => p.Id == group.First().ProductId);
                var item = _context.ProductCustomizationItems.Where(pci => group.Select(d => d.ItemId).Contains(pci.Id)).ToList();
                int groupTotal = (product.UnitPrice + item.Sum(pci => pci.UnitPrice)) * group.First().Qty;
                cartTotal += groupTotal;
            }

            if (memberWallet >= cartTotal)
            {
                memberWallet -= cartTotal;
                var member = _context.Members.First(m => m.Id == memberId);
                member.Balance = memberWallet;
                _context.Members.Update(member);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("會員帳戶餘額不足");
            }
        }

        public void CreateNewOrder(int memberId, int storeId, int fee, string address)
        {
            var cart = _context.Carts.First(c => c.Id == memberId && c.StoreId == storeId);
            var order = new OrderDTO
            {
                MemberId = cart.MemberId,
                StoreId = cart.StoreId,
                DeliveryAddress = address,
                DeliveryFee = fee,            
            };
            _context.Orders.Add(order.ToOrderEF());
            _context.SaveChanges();

            var createMark = _context.Orders.First(o => o.MemberId == cart.MemberId && o.StoreId == cart.StoreId && o.CreateMark == true);
            foreach (var detail in cart.CartDetails.OrderBy(cd => cd.IdentifyNum).ThenBy(cd => cd.ItemId))
            {
                var orderDetail = new OrderDetailDTO
                {
                    IdentifyNum = detail.IdentifyNum,
                    ProductId = detail.ProductId,
                    ProductPrice = _context.Products.First(p => p.Id == detail.ProductId).UnitPrice,
                    ItemId = detail.ItemId,
                    ItemPrice = _context.ProductCustomizationItems.First(pci => pci.Id == detail.ItemId).UnitPrice,
                    Qty = detail.Qty,
                    OrderId = createMark.Id,
                };
                _context.OrderDetails.Add(orderDetail.ToOrderDetailEF());                
            }

            var orderSechedule = new OrderScheduleDTO
            { 
                OrderId = createMark.Id,
                StatusId = _context.OrderStatues.Min().Id,
                MarkTime = DateTime.Now,
            };
            _context.OrderSchedules.Add(orderSechedule.ToOrderScheduleEF());

            createMark.CreateMark = false;
            _context.SaveChanges();           
        }

        public OrderDTO GetOrderTrack(long orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId).ToOrderDTO();
            var identifyGroup = order.Details.GroupBy(d => d.IdentifyNum); ;
            var orderDetail = identifyGroup.Select(gd => new OrderDetailDTO
            {
                IdentifyNum = gd.Key,
                ProductId = gd.First().ProductId,
                ProductName =_context.Products.First(p => p.Id == gd.First().ProductId).ProductName,
                ItemId = gd.Select(d => d.ItemId).ToList(),
            })


        }
    }
}
