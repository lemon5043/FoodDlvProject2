using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;

namespace FoodDlvProject2.Models.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderVM> Search()
        {
            return (IEnumerable<OrderVM>)_repository.Search();
        }


    }
}
