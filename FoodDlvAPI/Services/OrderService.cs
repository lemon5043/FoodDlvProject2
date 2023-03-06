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

        public OrderDTO OrderInfo(CartDTO cart, OrderDTO order)
        {
            return new OrderDTO();
        }
    }
}
