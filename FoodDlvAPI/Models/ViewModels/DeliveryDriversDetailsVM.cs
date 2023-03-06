using FoodDlvAPI.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvAPI.Models.ViewModels
{
    public class DeliveryDriversDetailsVM
    {
        public int Id { get; set; }
        [Display(Name = "帳號")]
        public string Account { get; set; }
        [Display(Name = "姓名")]
        public string DriverName => LastName + FirstName;
        [Display(Name = "姓")]
        public string LastName { get; set; }
        [Display(Name = "名")]
        public string FirstName { get; set; }
        [Display(Name = "聯絡電話")]
        public string Phone { get; set; }
        [Display(Name = "銀行帳戶")]
        public string BankAccount { get; set; }
        [Display(Name = "帳號認證狀態")]
        public string AccountStatus { get; set; }
        [Display(Name = "工作狀態")]
        public string WorkStatuse { get; set; }
        [Display(Name = "違規次數")]
        public int? DeliveryViolationRecords { get; set; }
        [Display(Name = "外送評分")]
        public double? DriverRating { get; set; }
        [Display(Name = "註冊時間")]
        public DateTime RegistrationTime { get; set; }
        [Display(Name = "身分證")]
        public string Idcard { get; set; }
        [Display(Name = "行照")]
        public string VehicleRegistration { get; set; }
        [Display(Name = "駕照")]
        public string DriverLicense { get; set; }
    }
    public static class DriversDtoDeliveryDriversDetailsVMExts
    {
        public static DeliveryDriversDetailsVM ToDeliveryDriversDetailsVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversDetailsVM
            {
                Id = source.Id,
                Account = source.Account,
                LastName = source.LastName,
                FirstName= source.FirstName,
                Phone = source.Phone,
                BankAccount = source.BankAccount,
                DriverRating = source.DriverRating,
                RegistrationTime = source.RegistrationTime,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
    }
}

