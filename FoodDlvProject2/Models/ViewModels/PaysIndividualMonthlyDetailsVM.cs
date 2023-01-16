using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    internal class PaysIndividualMonthlyDetailsVM
    {
        public int Id { get; set; }
        [Display(Name = "外送員編號")]
        public int DeliveryDriversId { get; set; }
        [Display(Name = "姓名")]
        public string DriversName { get; set; }
        [Display(Name = "外送達成次數")]
        public int DeliveryCount { get; set; }
        [Display(Name = "總里程數")]
        public int TotalMilage { get; set; }
        [Display(Name = "獎金")]
        public int Bouns { get; set; }
        [Display(Name = "實付薪資總額")]
        public int TotalPay { get; set; }
        [Display(Name = "結算月份")]
        public DateTime SettlementMonth { get; set; }
    }
}