using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Models.ViewModels
{
	public class MemberLocationVM
	{
		public int MemberId { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
	public static class MemberLocationVMExts
	{
		public static MemberLocationDto ToMemberLocationDto(this MemberLocationVM model)
			=> new MemberLocationDto
			{
				MemberId = model.MemberId,
				Longitude = model.Longitude,
				Latitude = model.Latitude,
			};
	}
}
