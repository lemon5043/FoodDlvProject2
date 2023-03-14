namespace FoodDlvAPI.Models.DTOs
{
	public class MemberLocationDto
	{
        public int Id { get; set; }
        public string Address { get; set; }
        public int MemberId { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
	public static class MemberLocationDtoExts
	{
		public static AccountAddress ToMemberEFModel(this MemberLocationDto dto)
		{
			return new AccountAddress
			{
				Id = dto.Id,
				Address = dto.Address,
				MemberId= dto.MemberId,
				Latitude = dto.Latitude,
				Longitude = dto.Longitude,
			};
		}
	}
}
