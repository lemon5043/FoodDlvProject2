using FoodDlvProject2.EFModels;
using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.ViewModels
{
    public class StorePrincipalVM
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
    public static class StorePrincipalsVMExt
    {
        public static StorePrincipal ToStorePrincipal(this StorePrincipalVM storePrincipalVM)
        {
            return new StorePrincipal
            {
                Id = storePrincipalVM.Id,
                AccountStatusId = storePrincipalVM.AccountStatusId,
                FirstName = storePrincipalVM.FirstName,
                LastName = storePrincipalVM.LastName,
                Phone = storePrincipalVM.Phone,
                Gender = storePrincipalVM.Gender,
                Birthday = storePrincipalVM.Birthday,
                Email = storePrincipalVM.Email,
                Account = storePrincipalVM.Account,
                Password = storePrincipalVM.Password,
                RegistrationTime = storePrincipalVM.RegistrationTime,

            };

        }
    }
    public static class StorePrincipalsExt
    {
        public static StorePrincipalVM ToStorePrincipaVM(this StorePrincipal storePrincipal)
        {
            return new StorePrincipalVM
            {
                Id = storePrincipal.Id,
                AccountStatusId = storePrincipal.AccountStatusId,
                FirstName = storePrincipal.FirstName,
                LastName = storePrincipal.LastName,
                Phone = storePrincipal.Phone,
                Gender = storePrincipal.Gender,
                Birthday = storePrincipal.Birthday,
                Email = storePrincipal.Email,
                Account = storePrincipal.Account,
                Password = storePrincipal.Password,
                RegistrationTime = storePrincipal.RegistrationTime,

            };

        }
    }
}
