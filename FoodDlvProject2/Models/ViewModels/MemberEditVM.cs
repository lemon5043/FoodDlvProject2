using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;
using FoodDlvProject2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
    public class MemberEditVM
    {
		public int Id { get; set; }
		[Display(Name = "會員狀態")]
        public int AccountStatusId { get; set; }
		[Required(ErrorMessage = "請輸入{0}")]
		[Display(Name = "名字")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "請輸入{0}")]
		[Display(Name = "姓氏")]
        public string LastName { get; set; }
		[Required(ErrorMessage = "請輸入{0}")]
		[Display(Name = "電話號碼")]
		[StringLength(10, ErrorMessage = "號碼不能大於{1}")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "請輸入{0}")]
		[Display(Name = "生日")]
		[DateNowAttribute(ErrorMessage = "{}不可大於今日")]
		public DateTime Birthday { get; set; }
		[EmailAddress(ErrorMessage = "輸入的{0}格式不正確")]
		[Display(Name = "信箱")]
        public string Email { get; set; }
		[Required(ErrorMessage = "請輸入{0}")]
		[Display(Name = "帳號")]
        public string Account { get; set; }
		
		public bool Gender { get; set; }

	}
    public static  class MemberEditVMExts
    {
        public static MemberEditVM ToMemberEditVM(this MemberDTO source)
        {
            return new MemberEditVM
            {

				Id=source.Id,
				AccountStatusId = source.AccountStatusId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Birthday = source.Birthday,
                Email = source.Email,
                Account = source.Account,
                Gender= source.Gender,

			};
        }
        public static MemberEditDTO ToMemberEditDTO(this MemberEditVM source)
        {
            return new MemberEditDTO
            {
				Id = source.Id,
				AccountStatusId = source.AccountStatusId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Phone = source.Phone,
                Birthday = source.Birthday,
                Email = source.Email,
                Account = source.Account,
				Gender = source.Gender,
			};

        }
    };
}
