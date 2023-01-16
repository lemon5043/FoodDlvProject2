using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    internal class DeliveryViolationRecordVM
    {
        public int Id { get; set; }
        [Display(Name = "外送員ID")]
        public int DriverId { get; set; }
        [Display(Name = "姓名")]
        public string DriverName { get; set; }
        [Display(Name = "違規")]
        public string Violation { get; set; }
        [Display(Name = "違規內容")]
        public string Content { get; set; }
        [Display(Name = "違規日期")]
        public DateTime ViolationDate { get; set; }
    }
}