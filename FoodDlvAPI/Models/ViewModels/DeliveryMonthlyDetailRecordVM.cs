using FoodDlvAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryMonthlyDetailRecordVM
    {
        public long Id { get; set; }
        [Display(Name = "結算月份")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "總里程數")]
        public decimal TotalMilage { get; set; }
        [Display(Name = "總外送次數")]
        public int TotalDelievery { get; set; }
        [Display(Name = "外送員姓名")]
        public string DriverName { get; set; }
    }
    public static partial class DeliveryRecordDTOExts
    {
        public static DeliveryMonthlyDetailRecordVM ToDeliveryMonthlyDetailRecordVM(this DeliveryRecordDTO source)
        {
            return new DeliveryMonthlyDetailRecordVM
            {
                Id = source.Id,
                OrderDate = source.OrderDate,
                TotalMilage = source.TotalMilage,
                TotalDelievery = source.TotalDelievery,
                DriverName = source.DriverName,
            };
        }
    }
}
