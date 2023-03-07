using FoodDlvAPI.DTOs;
using FoodDlvAPI.Interfaces;

namespace FoodDlvAPI.Services
{
    public class OrderService
    {
        //Fields
        private readonly IOrderRepository _orderRepository;        
        

        //Constructors
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;            
        }

        public OrderDTO OrderInfo(long cartId, string address, int fee)
        {
            var orderInfo = _orderRepository.GetOrderInfo(cartId, address, fee);
            return orderInfo;            
        }

        public void CheckOutTime(int storeId)
        {
            _orderRepository.CheckOutTime(storeId);
        }

        public void OrderEstablished(long memberId, int storeId, int fee)
        {
            _orderRepository.CashTransfer(memberId, storeId, fee);
            _orderRepository.
        }
    }
}
