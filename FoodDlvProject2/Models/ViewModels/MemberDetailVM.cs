using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvProject2.Models.ViewModels
{
	public class MemberDetailVM
    {

		[Display(Name = "會員編號")]
		public int Id { get; set; }
		[Display(Name = "會員狀態")]
		public int AccountStatusId { get; set; }
		[Display(Name = "姓名")]
		public string MemberName { get; set; }
		[Display(Name = "電話")]
		public string Phone { get; set; }
		[Display(Name = "性別")]
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
		public string Password { get; set; }
		[Display(Name = "註冊時間")]
		public DateTime RegistrationTime { get; set; }

	}
	public static partial class MembersDtoExts
	{
		public static MemberDetailVM ToMemberDetailVM(this MemberDTO source)
		{
			return new MemberDetailVM
			{
				Id = source.Id,
				AccountStatusId = source.AccountStatusId,
				MemberName = source.LastName + source.FirstName,
				Phone = source.Phone,
				Gender = source.Gender,
				Birthday = source.Birthday,
				Email = source.Email,
				Balance = source.Balance,
				Account = source.Account,
				Password = source.Password,
				RegistrationTime = source.RegistrationTime,
			};
		}
	}
}
