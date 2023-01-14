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
            IEnumerable<OrderDto> data = await _repository.SearchAsync(start, end, keyWord);

			//把orderSchedule每一筆資料取出, 放到新的List<OrderScheduleDto>,
			//在把新的List<OrderScheduleDto>的資料分別給OrderDto的OrderTime與orderSchedule屬性
			foreach (var orderData in data)
            {
                var orderScheduleData = orderData.orderSchedule
                    .Where(os => os.StatusId == 1)
                    .Select(os => new OrderScheduleDto
                    {
                        StatusId = os.StatusId,
                        MarkTime = os.MarkTime,
                    }).ToList();
                orderData.OrderTime = orderScheduleData.FirstOrDefault().MarkTime;
                orderData.orderSchedule = orderScheduleData;
            }

            return data;
        }

        public IEnumerable<OrderDetailDto> DetailSearch(long orderId)
        {
            return _repository.DetailSearch(orderId);
        }

        public IEnumerable<OrderProductDto> ProductSearch(long productId)
        {
            return _repository.ProductSearch(productId);
        }

    }
}
