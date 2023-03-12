using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.ViewModels
{
	public class MemberForgetPWEditVM
	{
		public int Id { get; set; }
		public string Password { get; set; }
	}
	public static class MemberForgetPWEditVMExts
	{
		public static MemberForgetPWEditVM ToMemberForgetPWVM(this MemberDTO source)
		{
			return new MemberForgetPWEditVM
			{
				Id = source.Id,
				Password = source.Password,

			};
		}
	}
}
