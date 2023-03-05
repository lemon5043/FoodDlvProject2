using FoodDlvAPI.Infrastructures;

namespace FoodDlvAPI.Models.DTOs
{
	public class MemberRegisterDto : MemberEncryptedPassword
	{
		public string Password { get; set; }
		public string EncryptedPassword
		{
			get
			{
				string salt = SALT;
				string result = HashUtility.ToSHA256(this.Password, salt);
				return result;
			}
		}
	}
	public class MemberEncryptedPassword
	{
		public const string SALT = "!==#DACAEGYT";
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
	public static class MemberEncryptedExts
	{
		public static Member ToMember(this MemberRegisterDto source)
		{
			return new Member
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
				Password = source.EncryptedPassword,
				RegistrationTime = source.RegistrationTime,
			};
		}
		public static MemberRegisterDto ToEntity(this Member entity)
		{
			if (entity == null) return null;
			return new MemberRegisterDto
			{
				Id = entity.Id,
				AccountStatusId = entity.AccountStatusId,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				Phone = entity.Phone,
				Gender = entity.Gender,
				Birthday = entity.Birthday,
				Email = entity.Email,
				Balance = entity.Balance,
				Account = entity.Account,
				Password = entity.Password,
				RegistrationTime = entity.RegistrationTime,

			};
		}
	}
}
