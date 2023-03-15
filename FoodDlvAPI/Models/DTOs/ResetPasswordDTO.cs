namespace FoodDlvAPI.Models.DTOs
{
	public class ResetPasswordDTO
	{
		public int memberid { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
