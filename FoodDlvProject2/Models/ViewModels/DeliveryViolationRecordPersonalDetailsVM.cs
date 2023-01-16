using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    internal class DeliveryViolationRecordPersonalDetailsVM
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
}