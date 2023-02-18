using FoodDlvProject2.Models.DTOs;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class OrderScheduleVM
    {
		[Display(Name = "店家編號")]
		public long StoreId { get; set; }

		[Display(Name = "外送員編號")]
		public long DeliveryDriverId { get; set; }

		[Display(Name = "外送地址")]
		public string DeliveryAddress { get; set; }

		public OrderScheduleStatus ScheduleStatus { get; set; }
	}

	public static partial class OrderScheduleDtoExts
	{
		public static OrderScheduleVM ToOrderScheduleVM(this OrderScheduleDto source)
		{
			return new OrderScheduleVM
			{
				StoreId = source.StoreId,
				DeliveryDriverId = source.DeliveryDriverId,
				DeliveryAddress = source.DeliveryAddress,
				ScheduleStatus = source.ScheduleStatus,
			};
		}
	}
}
