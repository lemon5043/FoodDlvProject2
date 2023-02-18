using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryIndividualDetailsRecordVM
    {
        [Display(Name ="訂單編號")]
        public long Id { get; set; }
        [Display(Name = "訂單最後狀態時間")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "外送公里數")]
        public decimal Milage { get; set; }
        [Display(Name ="訂單狀態")]
        public string Status { get; set; }
		[Display(Name = "外送員姓名")]
		public string DriverName { get; set; }
	}

    public static partial class DeliveryRecordDTOExts
    {
        public static DeliveryIndividualDetailsRecordVM ToDeliveryIndividualDetailsRecordVM(this DeliveryRecordDTO source)
        {
            return new DeliveryIndividualDetailsRecordVM
            {
                Id = source.Id,
                OrderDate = source.OrderDate,
                Milage = source.Milage,
                Status = source.Status,
                DriverName = source.DriverName,
            };
        }
    }
}