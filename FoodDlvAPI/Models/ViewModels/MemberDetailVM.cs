using FoodDlvAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDlvAPI.Models.ViewModels
{
	public class MemberDetailVM
	{
		public int Id { get; set; }
		public int AccountStatusId { get; set; }
		[Display(Name = "名字")]
		public string FirstName { get; set; }
		[Display(Name = "姓氏")]
		public string LastName { get; set; }
		[Display(Name = "手機號碼")]
		public string Phone { get; set; }

		[Display(Name = "帳號")]
		public string Account { get; set; }
		[Display(Name = "密碼")]
		public string Password { get; set; }
		[Display(Name = "註冊時間")]
		public DateTime RegistrationTime { get; set; }
	}
	public static class MemberDetailVMExts
	{
		public static MemberDetailVM ToMemberDetailVM(this MemberDTO source)
		{
			return new MemberDetailVM
			{
				Id = source.Id,
				AccountStatusId = source.AccountStatusId,
				FirstName = source.FirstName,
				LastName = source.LastName,
				Phone = source.Phone,
				Account = source.Account,
				Password = source.Password,
				RegistrationTime = source.RegistrationTime,
			};
		}
	}
}
