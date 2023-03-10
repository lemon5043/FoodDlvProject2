using FoodDlvAPI.EFModels;
using FoodDlvAPI.Models.Entitys;
using FoodDlvAPI.Models.Infrastructures.ExtensionMethods;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvAPI.Models.ViewModels
{
    public class DeliveryDriversEditVM
    {
        public int Id { get; set; }

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

        [ExtensionAttribute(".jpg", ".png", ".jepg", ErrorMessage = "{0}的格式必須為.jpg、.png或.jepg格式")]
        [Display(Name = "身分證")]
        public IFormFile? Idcard { get; set; }

        [Display(Name = "行照")]
        public IFormFile? VehicleRegistration { get; set; }


        [Display(Name = "駕照")]
        public IFormFile? DriverLicense { get; set; }

  
    }
    public static class DeliveryDriversEditVMExts
    {
        public static DeliveryDriverEntity ToDeliveryDriverEntity(this DeliveryDriversEditVM source)
        {
            return new DeliveryDriverEntity
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                BankAccount = source.BankAccount,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
    }
}
