using FoodDlvProject2.EFModels;
using System.Collections.Generic;


namespace FoodDlvProject2.Models.DTOs
{
    public class OrderScheduleDto
    {
		public OrderScheduleStatus ScheduleStatus { get; set; }		
		public long StoreId { get; set; }
		public long DeliveryDriverId { get; set; }
		public string DeliveryAddress { get; set; }
	}

	public class OrderScheduleStatus
	{
		public string Status { get; set; }
		public int StatusId { get; set; }
		public DateTime MarkTime { get; set; }
	}
}
