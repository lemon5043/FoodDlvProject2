using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Models.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public int AccountStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
    public static class MemberExts
    {
        public static MemberDTO ToEntity(this MemberDTO source)
       => new MemberDTO
       {
           Id = source.Id,
           AccountStatusId = source.AccountStatusId,
           FirstName = source.FirstName,
           LastName = source.LastName,
           Phone = source.Phone,
           Gender = source.Gender,
           Birthday = source.Birthday,
           Email = source.Email,
           Balance = source.Balance,
           Account = source.Account,
           Password = source.Password,
           RegistrationTime = source.RegistrationTime,
       };


    }
}
