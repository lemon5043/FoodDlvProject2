namespace FoodDlvAPI.DTOs
{
	public class StoreLoginresponse
	{

		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public int Id { get; set; }
		public string Username { get; set; }

		public static StoreLoginresponse Success(int id, string userName)
			=> new StoreLoginresponse { IsSuccess = true, Id = id, Username = userName };

		public static StoreLoginresponse Fail(string errorMessage)
			=> new StoreLoginresponse { IsSuccess = false, ErrorMessage = errorMessage };
	}
}
