using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;
using FoodDlvAPI.Models;

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
            var timeRange =  targetStore.FirstOrDefault(sbh => sbh.OpeningDays == nowDay && sbh.OpeningTime <= nowTime && sbh.ClosingTime <= nowTime);
           
            if (timeRange == null)
            {
                throw new Exception("目前非商家營業時間");
            }
        }

        public void CashTransfer(long memberId, int storeId, int fee)
        {
            //測試資料
            var memberWallet = 1000;

            var cart = _context.Carts.First(c => c.MemberId == memberId && c.StoreId == storeId);
            var identifyGroup = cart.CartDetails.GroupBy(d => d.IdentifyNum).ToList();
            var cartTotal = cart.Sum(
                           _context.Products.Single(p => p.Id == identifyGroup.First().ProductId).UnitPrice +
                           _context.ProductCustomizationItems.Where(pci => identifyGroup.Select(d => d.ItemId).Contains(pci.Id)).Sum(pci => pci.UnitPrice)
                           ) * identifyGroup.First().Qty ;
        }

    }
}
