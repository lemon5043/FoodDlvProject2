using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
	public class OrderScheduleDto
	{
		public int Id { get; set; }
		public long OrderId { get; set; }
		public int StatusId { get; set; }
		public DateTime MarkTime { get; set; }

		//public OrderScheduleDto(int id, long orderId, int statusId, DateTime markTime)
		//{
		//	Id = id;
		//	OrderId = orderId;
		//	StatusId = statusId;
		//	MarkTime = markTime;
		//}
	}

	public static partial class OrderScheduleExts
	{
		public static OrderScheduleDto ToOrderScheduleEntity(this OrderSchedule source)
		{
			return new OrderScheduleDto 
			{ 
				Id = source.Id,
                OrderId = source.OrderId,
                StatusId = source.StatusId,
                MarkTime = source.MarkTime, 
			};
		}
	}
}
