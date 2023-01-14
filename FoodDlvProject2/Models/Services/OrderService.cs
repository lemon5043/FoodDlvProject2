using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using X.PagedList;

namespace FoodDlvProject2.Models.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDto>> SearchAsync(DateTime? start, DateTime? end, string keyWord)
        {
            return await _repository.SearchAsync(start, end, keyWord);
        }

        public IEnumerable<OrderDetailDto> DetailSearch(long orderId)
        {
            return _repository.DetailSearch(orderId);
        }

    }
}
