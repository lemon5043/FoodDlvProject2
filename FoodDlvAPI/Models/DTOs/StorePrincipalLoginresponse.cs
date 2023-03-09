namespace FoodDlvAPI.Models.DTOs
{
	public class StorePrincipalLoginresponse
	{
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public int Id { get; set; }
		public string Username { get; set; }

		public static StorePrincipalLoginresponse Success(int id, string userName)
			=> new StorePrincipalLoginresponse { IsSuccess = true, Id = id, Username = userName };

		public static StorePrincipalLoginresponse Fail(string errorMessage)
			=> new StorePrincipalLoginresponse { IsSuccess = false, ErrorMessage = errorMessage };
	}
}
