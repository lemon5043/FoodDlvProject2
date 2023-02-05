using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryViolationRecordEditVM
    {
        public int Id { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "外送員編號")]
        public int DriverId { get; set; }
		[Display(Name = "違規事項")]
		public int ViolationId { get; set; }
        [Display(Name = "外送員姓名")]
		public string DriverName { get; set; }
        [Display(Name = "違規日期")]
        [Required(ErrorMessage ="請輸入{0}")]
        [DateNowAttribute(ErrorMessage = "輸入{0}不可大於今日")]
        public DateTime ViolationDate { get; set; }

    }
    public static class DeliveryViolationRecordEditVMExts
    {
        public static DeliveryViolationRecordEditVM ToDeliveryViolationRecordEditVM(this DeliveryViolationRecordDTO source)
        {
            return new DeliveryViolationRecordEditVM
            {
                Id = source.Id,
                OrderId=source.OrderId,
                DriverId=source.DeliveryDriversId,
                ViolationId = source.ViolationId,
                DriverName =source.DriverName,
                ViolationDate = source.ViolationDate,
            };
        }

        public static DeliveryViolationRecordDTO ToDeliveryViolationRecordDTO(this DeliveryViolationRecordEditVM source)
        {
            return new DeliveryViolationRecordDTO
            {
                Id=source.Id,
				ViolationId=source.ViolationId,
				ViolationDate=source.ViolationDate,
			};
		}
	}
}