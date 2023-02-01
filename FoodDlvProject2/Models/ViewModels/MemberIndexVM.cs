using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class MemberIndexVM
    {
        [Display(Name = "會員編號")]
        public int Id { get; set; }
        [Display(Name = "會員狀態")]
        public int AccountStatusId { get; set; }
        [Display(Name = "姓名")]
        public string MemberName { get; set; }
        [Display(Name = "電話號碼")]
        public string Phone { get; set; }
        [Display(Name = "會員開通狀態")]
        public bool Gender { get; set; }
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "信箱")]
        public string Email { get; set; }
        [Display(Name = "餘額")]
        public int Balance { get; set; }
        [Display(Name = "帳號")]
        public string Account { get; set; }
        [Display(Name = "密碼")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "註冊時間")]
        public DateTime RegistrationTime { get; set; }
    }
    public static partial class MemberIndexVMExts 
    {
        public static MemberIndexVM ToMemberIndexVM(this MemberDTO source)
        {
            return new MemberIndexVM
            {
                Id = source.Id,
                AccountStatusId=source.AccountStatusId,
                MemberName=source.FirstName+source.LastName,
                Phone=source.Phone,
                Gender=source.Gender,
                Birthday=source.Birthday,
                Email=source.Email,
                Balance=source.Balance,
                Account=source.Account,
                Password=source.Password
            };
        }
    }
   
}
