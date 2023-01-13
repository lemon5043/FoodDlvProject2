using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryIndividualDetailsRecordVM
    {
        [Display(Name ="訂單編號")]
        public long Id { get; set; }
        [Display(Name = "訂單最後狀態日期")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "外送公里數")]
        public decimal Milage { get; set; }
        [Display(Name ="訂單狀態")]
        public string Status { get; set; }
    }
}