using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DriverCancellationRecordsDetailsVM
    {
        public int Id { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "外送員編號")]
        public int DriverId { get; set; }
        [Display(Name = "姓名")]
        public string DriverName { get; set; }
        [Display(Name = "取消原因")]
        public string Reason { get; set; }
        [Display(Name = "取消原因簡述")]
        public string Context { get; set; }
        [Display(Name = "取消日期")]
        public DateTime CancellationDate { get; set; }
    }

	public static class DriverCancellationRecordsDetailsVMExts
	{
		public static DriverCancellationRecordsDetailsVM ToDeliveryViolationRecordsIndexVM(this DriverCancellationRecordDTO source)
		{
			return new DriverCancellationRecordsDetailsVM
			{
				Id = source.Id,
				OrderId = source.OrderId,
				DriverId = source.DriverId,
				DriverName = source.DriverName,
				Reason = source.Reason,
                Context= source.Context,
				CancellationDate = source.CancellationDate,
			};
		}
	}
}