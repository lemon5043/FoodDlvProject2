namespace FoodDlvAPI.Models.DTOs
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static LoginResponse Success() => new LoginResponse { IsSuccess = true };

        public static LoginResponse Fail(string errorMessage)
            => new LoginResponse { IsSuccess = false, ErrorMessage = errorMessage };

    }
}
