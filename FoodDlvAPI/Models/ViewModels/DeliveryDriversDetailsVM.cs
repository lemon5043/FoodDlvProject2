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
        public string DriverName { get; set; }
        [Display(Name = "性別")]
        public bool Gender { get; set; }
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "聯絡電話")]
        public string Phone { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
        [Display(Name = "銀行帳戶")]
        public string BankAccount { get; set; }
        [Display(Name = "帳號認證狀態")]
        public string AccountStatus { get; set; }
        [Display(Name = "工作狀態")]
        public string WorkStatuse { get; set; }
        [Display(Name = "違規次數")]
        public int? DeliveryViolationRecords { get; set; }
        [Display(Name = "外送評分")]
        public string? DriverRating { get; set; }
        [Display(Name = "註冊時間")]
        public DateTime RegistrationTime { get; set; }
        [Display(Name = "身分證")]
        public string Idcard { get; set; }
        [Display(Name = "行照")]
        public string VehicleRegistration { get; set; }
        [Display(Name = "駕照")]
        public string DriverLicense { get; set; }
    }
    public static partial class DriversDtoExts
    {
        public static DeliveryDriversDetailsVM ToDeliveryDriversDetailsVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversDetailsVM
            {
                Id = source.Id,
                Account = source.Account,
                DriverName = source.LastName + source.FirstName,
                Gender = source.Gender,
                Birthday = source.Birthday,
                Phone = source.Phone,
                Email = source.Email,
                BankAccount = source.BankAccount,
                AccountStatus = source.AccountStatus,
                WorkStatuse = source.WorkStatuse,
                DeliveryViolationRecords = source.DeliveryViolationRecords,
                DriverRating = string.IsNullOrEmpty(string.Format("{0:N1}", source.DriverRating)) ? "0.0" : string.Format("{0:N1}", source.DriverRating),
                RegistrationTime = source.RegistrationTime,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
    }
}

