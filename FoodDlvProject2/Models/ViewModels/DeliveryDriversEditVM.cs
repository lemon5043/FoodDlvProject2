using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDlvProject2.Models.ViewModels
{
    public class DeliveryDriversEditVM
    {
        public int Id { get; set; }

        [Display(Name = "帳號認證狀態")]
        public int AccountStatusId { get;  set; }

        [Display(Name = "工作狀態")]
        public int WorkStatuseId { get;  set; }

        [Required(ErrorMessage ="請輸入{0}")]
        [Display(Name = "名子")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "姓氏")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "聯絡電話")]
        [StringLength(10, ErrorMessage = "字串長度不能大於{1}")]
        public string? Phone { get; set; }

        [Display(Name = "性別")]
        public bool Gender { get; set; }

        [Required(ErrorMessage ="請輸入{0}")]
        [Display(Name = "銀行帳戶")]
        public string? BankAccount { get; set; }

        [ExtensionAttribute(".jpg",".png",".jepg",ErrorMessage = "{0}的格式必須為.jpg、.png或.jepg格式")]
        [Display(Name = "身分證")]
		public IFormFile? Idcard { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [Display(Name = "生日")]
        [DateNowAttribute(ErrorMessage = "{0}不可大於今日")]
        public DateTime Birthday { get; set; }

        [Display(Name = "電子郵件")]
        [EmailAddress(ErrorMessage ="輸入的{0}格式不正確")]
        public string? Email { get; set; }
        
        [Display(Name = "行照")]		
		public IFormFile? VehicleRegistration { get; set; }

        [Display(Name = "駕照")]
        public IFormFile? DriverLicense { get; set; }
    }
    public static class DeliveryDriversEditVMExts
	{
        public static DeliveryDriversEditVM ToDeliveryDriversEditVM(this DeliveryDriverDTO source)
        {
            return new DeliveryDriversEditVM
            {
                Id = source.Id,
                AccountStatusId = source.AccountStatusId,
                WorkStatuseId = source.WorkStatuseId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Gender = source.Gender,
                BankAccount = source.BankAccount,
                Birthday = source.Birthday,             
                Email = source.Email,

            };
        }
        public static DeliveryDriverEditDTO ToDeliveryDriverEditDTO(this DeliveryDriversEditVM source)
        {
            return new DeliveryDriverEditDTO
            {
                Id = source.Id,
                AccountStatusId = source.AccountStatusId,
                WorkStatuseId = source.WorkStatuseId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Gender = source.Gender,
                BankAccount = source.BankAccount,
                Birthday = source.Birthday,
                Email = source.Email,
                Idcard = source.Idcard,
                VehicleRegistration = source.VehicleRegistration,
                DriverLicense = source.DriverLicense,
            };
        }
    }
}
