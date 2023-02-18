using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryViolationRecordDetailsVM
    {
        public int Id { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name = "外送員姓名")]
        public string DriverName { get; set; }
        [Display(Name = "違規事項")]
        public string ViolationContent { get; set; }
        [Display(Name = "違規內容")]
        public string Content { get; set; }
        [Display(Name = "違規日期")]
        public DateTime ViolationDate { get; set; }
    }

    public static class DeliveryViolationRecordDetailsVMExts
    {
        public static DeliveryViolationRecordDetailsVM ToDeliveryViolationRecordDetailsVM(this DeliveryViolationRecordDTO source)
        {
            return new DeliveryViolationRecordDetailsVM
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
}