using FoodDlvProject2.EFModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class StorePrincipalCreateVM
    {
        public int Id { get; set; }

        [Display(Name = "帳號狀態")]
        public int AccountStatusId { get; set; }

        [Display(Name = "名字")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "姓氏")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(10)]
        public string Phone { get; set; }

        [Display(Name = "性別")]
        [Required]

        public bool Gender { get; set; }

        [Display(Name = "生日")]
        [Required(ErrorMessage = "生日必填")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "帳號")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string Account { get; set; }

        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(30)]
        public string Password { get; set; }

        [Display(Name = "確認密碼")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [StringLength(30)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "註冊時間")]
        public DateTime RegistrationTime { get; set; }
    }
   
   
}
