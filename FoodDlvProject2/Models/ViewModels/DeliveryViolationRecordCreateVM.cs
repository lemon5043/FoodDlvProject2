using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryViolationRecordCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "外送員編號")]
        public int DriverId { get; set; }
        [Display(Name = "違規事項")]
        public int ViolationId { get; set; }

        [Display(Name = "違規日期")]
        [Required(ErrorMessage = "請輸入{0}")]
        [DateNowAttribute(ErrorMessage ="輸入{0}不可大於今日")]
        public DateTime ViolationDate { get; set; }

    }
    public static class DeliveryViolationRecordCreateVMExts
    {
        public static DeliveryViolationRecordDTO ToDeliveryViolationRecordDTO(this DeliveryViolationRecordCreateVM source)
        {
            return new DeliveryViolationRecordDTO
            {
                Id = source.Id,
                OrderId = source.OrderId,
                DeliveryDriversId = source.DriverId,
                ViolationId = source.ViolationId,
                ViolationDate = source.ViolationDate,
            };
        }

    }
}
