using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    internal class DeliveryViolationRecordsIndexVM
    {
        public int Id { get; set; }
        [Display(Name ="外送員編號")]
        public int DriverId { get; set; }
        [Display(Name = "姓名")]
        public string DriverName { get; set; }
        [Display(Name = "訂單編號")]
        public long OrderId { get; set; }
        [Display(Name ="違規事項")]
        public string ViolationContent { get; set; }
        [Display(Name ="違規日期")]
        public DateTime ViolationDate { get; set; }
    }
}