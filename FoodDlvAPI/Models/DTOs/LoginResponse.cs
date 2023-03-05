namespace FoodDlvAPI.Models.DTOs
{
    public class LoginResponse
    {
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public int Id { get; set; }
		public string Username { get; set; }
		public string EncryptedPassword { get; set; }

		public static LoginResponse Success(int id, string userName, string encryptedPassword)
			=> new LoginResponse { IsSuccess = true, Id = id, Username = userName, EncryptedPassword = encryptedPassword };

		public static LoginResponse Fail(string errorMessage)
			=> new LoginResponse { IsSuccess = false, ErrorMessage = errorMessage };

	}
}
