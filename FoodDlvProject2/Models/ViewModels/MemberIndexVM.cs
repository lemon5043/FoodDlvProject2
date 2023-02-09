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
        
        [Display(Name = "帳號")]
        public string Account { get; set; }
        
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
                Account=source.Account,
                RegistrationTime=source.RegistrationTime,

            };
        }
    }
   
}
