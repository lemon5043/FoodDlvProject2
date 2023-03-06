using FoodDlvAPI.Models.DTOs;
using FoodDlvAPI.Models.Entitys;
using FoodDlvAPI.Models.Infrastructures.ExtensionMethods;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvAPI.Models.ViewModels
{
    public class DeliveryDriverCreateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
		[EmailAddress(ErrorMessage = "輸入的{0}格式不正確")]
		public string Account { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "名子")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "姓氏")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "聯絡電話")]
        [StringLength(10, ErrorMessage = "字串長度不能大於{1}")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "銀行帳戶")]
        public string? BankAccount { get; set; }

        //[ExtensionAttribute(".jpg", ".png", ".jepg", ErrorMessage = "{0}的格式必須為.jpg、.png或.jepg格式")]
        [Display(Name = "身分證")]
        public IFormFile? Idcard { get; set; }

        [Display(Name = "行照")]
        public IFormFile? VehicleRegistration { get; set; }

        [Display(Name = "駕照")]
        public IFormFile? DriverLicense { get; set; }

        public DateTime RegistrationTime => DateTime.Now;


    }
    public static class DeliveryDriverCreateVMExts
    {
        public static DeliveryDriverEntity ToDeliveryDriverEditDTO(this DeliveryDriverCreateVM source)
        {
            return new DeliveryDriverEntity
            {
                Id = source.Id,
                Account= source.Account,
                Password= source.Password,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                BankAccount = source.BankAccount,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
                RegistrationTime= source.RegistrationTime,
            };
        }
    }
}