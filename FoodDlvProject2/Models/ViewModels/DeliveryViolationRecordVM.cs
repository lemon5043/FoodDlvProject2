using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
	public class DeliveryViolationRecordDeleteVM
	{
		public int Id { get; set; }
		[Display(Name = "訂單編號")]
		public long OrderId { get; set; }
		[Display(Name = "外送員編號")]
		public int DriverId { get; set; }
		[Display(Name = "姓名")]
		public string DriverName { get; set; }
		[Display(Name = "違規")]
		public string ViolationContent { get; set; }
		[Display(Name = "違規內容")]
		public string Content { get; set; }
		[Display(Name = "違規日期")]
		public DateTime ViolationDate { get; set; }
	}

	public static class DeliveryViolationRecordVMExts
	{
		public static DeliveryViolationRecordDeleteVM ToDeliveryViolationRecordDeleteVM(this DeliveryViolationRecordDTO source)
		{
			return new DeliveryViolationRecordDeleteVM
			{
				Id = source.Id,
				OrderId = source.OrderId,
				DriverId = source.DeliveryDriversId,
				DriverName = source.DriverName,
				ViolationContent = source.ViolationContent,
				Content = source.Content,
				ViolationDate = source.ViolationDate,
			};
		}
	}
}