using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Interfaces;

namespace FoodDlvAPI.Models.Services
{
    public class OrderService
    {
        //Fields
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;


        //Constructors
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
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

        public void OrderEstablished(int memberId, int storeId, int fee, string address)
        {
            _orderRepository.CashTransfer(memberId, storeId, fee);
            _orderRepository.CreateNewOrder(memberId, storeId, fee, address);
            _cartRepository.EmptyCart(memberId, storeId);
        }

        public OrderDTO OrderTracking(long orderId)
        {
            var orderTracking = _orderRepository.GetOrderTrack(orderId);
            return orderTracking;
        }
    }
}
