using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class MemberEditVM
    {
       
        [Display(Name = "會員狀態")]
        public int AccountStatusId { get; set; }
        [Display(Name = "姓氏")]
        public string FirstName { get; set; }
        [Display(Name = "名字")]
        public string LastName { get; set; }
        [Display(Name = "電話號碼")]
        public string Phone { get; set; }
     
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "信箱")]
        public string Email { get; set; }
    
        [Display(Name = "帳號")]
        public string Account { get; set; }

    }
    public static partial class MemberIndexVMExts
    {
        public static MemberEditVM ToMemberEditVM(this MemberDTO source)
        {
            return new MemberEditVM
            {
               
                AccountStatusId = source.AccountStatusId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Birthday = source.Birthday,
                Email = source.Email,
                Account = source.Account,
            };
        }
        public static MemberEditDTO ToMemberEditDTO(this MemberEditVM source)
        {
            return new MemberEditDTO
            {
                
                AccountStatusId = source.AccountStatusId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Birthday = source.Birthday,
                Email = source.Email,
                Account = source.Account,
            };

        }
    };
}
