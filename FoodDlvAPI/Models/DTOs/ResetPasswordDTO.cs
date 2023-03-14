namespace FoodDlvAPI.Models.DTOs
{
	public class ResetPasswordDTO
	{
		public string Account { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
