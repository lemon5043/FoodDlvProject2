namespace FoodDlvAPI.Models.DTOs
{
	public class GetMemberPositionDto
	{
		public long OrderId { get; set; }
		public string StoreAddress { get; set; }
		public int MemberId { get; set; }
		public string Address { get; set; }
		
		
	}
}
