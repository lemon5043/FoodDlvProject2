using FoodDlvProject2.EFModels;
using System.Collections.Generic;


namespace FoodDlvProject2.Models.DTOs
{
    public class OrderScheduleDto
    {
		public string Status { get; set; }
		public int StatusId { get; set; }
		public DateTime MarkTime { get; set; }		
	}
}
