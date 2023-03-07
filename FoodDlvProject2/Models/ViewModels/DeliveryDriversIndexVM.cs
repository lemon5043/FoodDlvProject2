using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryDriversIndexVM
    {
		[Display(Name = "外送員編號")]
		public int Id { get; set; }
        [Display(Name = "姓名")]
        public string DriverName { get; set; }
        [Display(Name = "性別")]
        public bool Gender { get; set; }
        [Display(Name = "帳號狀態")]
        public string AccountStatus { get; set; }
        [Display(Name = "工作狀態")]
        public string WorkStatuse { get; set; }
    }

    public static partial class DriversDtoExts
    {
        public static DeliveryDriversIndexVM ToDeliveryDriversIndexVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversIndexVM
            {
                Id = source.Id,
                DriverName = source.LastName + source.FirstName,
                AccountStatus = source.AccountStatus,
                WorkStatuse = source.WorkStatuse,
            };
        }
    }
}