using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryViolationRecordPersonalDetailsVM
    {
        public int Id { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "姓名")]
        public string DriverName { get; set; }
        [Display(Name = "違規事項")]
        public string ViolationContent { get; set; }
        [Display(Name = "違規內容")]
        public string Content { get; set; }
        [Display(Name = "違規日期")]
        public DateTime ViolationDate { get; set; }
    }
    
    public static class DeliveryViolationRecordPersonalDetailsVMExts
    {
        public static DeliveryViolationRecordPersonalDetailsVM ToDeliveryViolationRecordPersonalDetailsVM(this DeliveryViolationRecordDTO source)
            => new DeliveryViolationRecordPersonalDetailsVM
            {
                Id = source.Id,
                OrderId = source.OrderId,
                DriverName = source.DriverName,
                ViolationContent = source.ViolationContent,
                Content = source.Content,
                ViolationDate = source.ViolationDate,
            };
    }
}