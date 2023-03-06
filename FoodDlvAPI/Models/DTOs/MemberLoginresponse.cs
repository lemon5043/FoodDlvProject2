namespace FoodDlvAPI.Models.DTOs
{
    public class MemberLoginresponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }

        public static MemberLoginresponse Success(int id, string userName, string encryptedPassword)
            => new MemberLoginresponse { IsSuccess = true, Id = id, Username = userName, EncryptedPassword = encryptedPassword };

        public static MemberLoginresponse Fail(string errorMessage)
            => new MemberLoginresponse { IsSuccess = false, ErrorMessage = errorMessage };

    }
}
