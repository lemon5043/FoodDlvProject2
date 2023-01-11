using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
	public class OrderScheduleEntity
	{
		public int Id { get; set; }
		public long OrderId { get; set; }
		public int StatusId { get; set; }
		public DateTime MarkTime { get; set; }

		public OrderScheduleEntity(int id, long orderId, int statusId, DateTime markTime)
		{
			Id = id;
			OrderId = orderId;
			StatusId = statusId;
			MarkTime = markTime;
		}
	}

	public static partial class OrderScheduleExts
	{
		public static OrderScheduleEntity ToOrderScheduleEntity(this OrderSchedule source)
		{
			return new OrderScheduleEntity(source.Id, source.OrderId, source.StatusId, source.MarkTime);
		}
	}
}
