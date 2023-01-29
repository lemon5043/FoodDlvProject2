using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FoodDlvProject2.Models.Services
{
	public class OrderService
	{
		//Field
		private readonly IOrderRepository _repository;

		//Constructor
		public OrderService(IOrderRepository repository)
		{
			_repository = repository;
		}

		public async Task<OrderMainDto> OrderMain(string revenueRange, string exceptionOrderRange, string completedOrderRange)
		{
			var data = _repository.GetOrderMain(revenueRange, exceptionOrderRange, completedOrderRange);
			return null;
		}

		public async Task<IPagedList<OrderTrackingDto>> OrderTrackingAsync(DateTime? dateStart, DateTime? dateEnd,
																		   string searchItem, string keyWord,
																		   int pageSize, int pageNumber)
		{

			var data = _repository.GetOrderTrackingAsync(dateStart, dateEnd, searchItem, keyWord, pageSize, pageNumber);

			return await data;
		}

		
		public IEnumerable<SelectListItem> GetOrderTrackingSearchOptions(string searchItem)
		{
			var searchOption = new List<SelectListItem>
			{
				new SelectListItem{ Value = "0", Text = "請選擇"},
				new SelectListItem{ Value = nameof(OrderTrackingVM.Id), Text = "訂單編號"},
				new SelectListItem{ Value = nameof(OrderTrackingVM.MemberName), Text = "會員姓名"},
				new SelectListItem{ Value = nameof(OrderTrackingVM.StoreName), Text = "商家名稱"},
			};

			if(searchItem != null)
			{
				var selectItem = searchOption.SingleOrDefault(sli => sli.Value == searchItem);
				if(selectItem != null) selectItem.Selected = true;
			}
			return searchOption;
		}

		public async Task<IEnumerable<OrderScheduleDto>> OrderScheduleAsync(long id)
		{
			var data = _repository.GetOrderScheduleAsync(id);
			return await data;
		}

		public IEnumerable<OrderDetailDto> DetailSearch(long orderId)
		{
			return _repository.DetailSearch(orderId);
		}

		public IEnumerable<OrderProductDto> ProductSearch(long productId)
		{
			return _repository.ProductSearch(productId);
		}

		//把orderSchedule每一筆資料取出, 放到新的List<OrderScheduleDto>,
		//在把新的List<OrderScheduleDto>的資料分別給OrderDto的OrderTime與orderSchedule屬性
		//foreach (var orderData in data)
		//         {
		//             var orderScheduleData = orderData.orderSchedule
		//                 .Where(os => os.StatusId == 1)
		//                 .Select(os => new OrderScheduleDto
		//                 {
		//                     StatusId = os.StatusId,
		//                     MarkTime = os.MarkTime,
		//                 }).ToList();
		//             orderData.OrderTime = orderScheduleData.FirstOrDefault().MarkTime;
		//             orderData.orderSchedule = orderScheduleData;
		//         }

	}
}
